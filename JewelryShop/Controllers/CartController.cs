using JewelryShop.Application.Contracts;
using JewelryShop.Application.Interfaces;
using JewelryShop.Application.Services;
using JewelryShop.Helper;
using JewelryShop.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelryShop.Controllers
{
    [Route("")]

    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public CartController(IProductService productService, IOrderService orderService, IMapper mapper, IUserService userService)
        {

            _mapper = mapper;
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }


        [HttpGet("gio-hang")]
        public async Task<IActionResult> Index()
        {
            var serializedString = HttpContext.Session.GetString("P_ID");
            var listofproduct = new List<CartModel>();
            int quantity = 0;

            if (serializedString != null)
            {
                var listofid = JsonConvert.DeserializeObject<List<string>>(serializedString);
                var reallist =
                listofid.GroupBy(x => x)
                    .Select(x => new
                    {
                        id = x.Key,
                        count = x.Count()
                    }).ToList();

                if (listofid != null)
                {
                    foreach (var id in reallist)
                    {
                        var product = await _productService.GetProductDetails(new Guid(id.id));
                        product.FPrice = PriceFormatter.FormatPrice(product.Price);
                        listofproduct.Add(
                            new CartModel
                            {
                                count = id.count,
                                Product = product,
                                totalprice = PriceFormatter.FormatPrice(id.count * product.Price),
                            }
                            );

                    }
                }
            }
            return View("ShopCart", listofproduct);
        }

        [HttpPost]
        [Route("gio-hang")]
        public async Task<IActionResult> Index(List<string> id, List<int> count)
        {

            int countIndex = 0;
            var listofproduct = new List<CartModel>();

            var newListId = new List<string>();

            for (int i =0; i < id.Count; i++)
            {
                for(int j = 0; j < count[i]; j++)
                {
                    newListId.Add(id[i]);
                }
            }

            HttpContext.Session.SetString("P_ID", JsonConvert.SerializeObject(newListId).ToString());

            foreach (var i in id)
            {

                var product = await _productService.GetProductDetails(new Guid(i));
                product.FPrice = PriceFormatter.FormatPrice(product.Price);
                listofproduct.Add(
                    new CartModel
                    {
                        count = count.ElementAt(countIndex),
                        Product = product,
                        totalprice = PriceFormatter.FormatPrice(count.ElementAt(countIndex++) * product.Price),
                    }
                    );

            }
            return View("ShopCart", listofproduct);
        }


        [HttpPost("dat-hang")]
        public async Task<IActionResult> CheckOut(List<string> id, List<int> count)
        {
            var carviewModel = new CartViewModel();

            var phone = HttpContext.Session.GetString("phone");
            if (phone != null)
            {
                var user = await _userService.ManageAccount(phone);
                if (user != null) { carviewModel.Customer = user; }
            }
            else
            {
                TempData["ReturnUrl"] = "/gio-hang";
                return Redirect("/dang-nhap");
            }

            var listofproduct = new List<CartModel>();
            double total = 0;
            if (id != null)
            {
                int index = 0;
                foreach (var i in id)
                {
                    var product = await _productService.GetProductDetails(new Guid(i));
                    product.FPrice = PriceFormatter.FormatPrice(product.Price);
                    var currentprice = count.ElementAt(index) * product.Price;
                    listofproduct.Add(
                        new CartModel
                        {
                            count = count.ElementAt(index++),
                            Product = product,
                            totalprice = PriceFormatter.FormatPrice(currentprice),
                        }
                        );
                    total += currentprice;

                }
            }
            ViewBag.FTotal = PriceFormatter.FormatPrice(total);
            ViewBag.Total = (total);

            carviewModel.CartModels = listofproduct;

            return View("CheckOut", carviewModel);
        }

        [HttpPost("LastCheckOut")]
        public async Task<IActionResult> LastCheckOut(OrderViewModel order)
        {
            var listoforderDetailDto = new List<OrderDetailDto>();

            var listofproduct = new List<ProductDto>();
            int index = 0;

            if (order == null)
            {
                return Redirect("/");
            }
            foreach (var o in order.id)
            {
                var product = await _productService.GetProductDetails(new Guid(o));

                listofproduct.Add(product);

                var obj = _mapper.Map<ProductOrderDto>(product);
                var quan = order.count.ElementAt(index++);
                listoforderDetailDto.Add(
                    new OrderDetailDto()
                    {
                        ProductOrderDto = obj,
                        Quantity = quan,
                        TotalPrice = quan * obj.Price
                    }
                    );
            }

            var orderDto = new OrderDto()
            {
                Customer = new CustomerDto()
                {
                    Name = order.name,
                    Phone = order.phone,
                    Address = order.address + ", " + order.ward + ", " + order.district + ", " + order.city,

                },
                orderDetailDtos = listoforderDetailDto,
                Date = DateTime.Now,
                Status = Domain.Shared.Const.OrderStatus.Pending,
                Total = order.total
            };

            await _orderService.AddOrder(orderDto);


            return View("OrderSuccess");
        }
    }
}
