using JewelryShop.Application.Contracts;
using JewelryShop.Application.Interfaces;
using JewelryShop.Domain.Entities;
using JewelryShop.Domain.Repository;
using MapsterMapper;


namespace JewelryShop.Application.Services
{
    public class OrderService : IOrderService
    {

        //set up related repository using Dependency Injection
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SubCategory> _subcategoryRepository;
        private readonly IRepository<Warranty> _warrantyRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IOrderDetailRepository _orderdetailsRepository;


        private readonly IMapper _mapper;

        public OrderService(
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository,
            IRepository<SubCategory> subcategoryRepository,
            IRepository<Warranty> warrantyRepository,
            IRepository<Order> orderRepository,
            IOrderDetailRepository orderdetailsRepository,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _warrantyRepository = warrantyRepository;
            _orderRepository = orderRepository;
            _orderdetailsRepository = orderdetailsRepository;
            _customerRepository = customerRepository;

        }

        public async Task<bool> AddOrder(OrderDto orderdto)
        {
            var getUser = await _customerRepository.GetCustomerByPhone(orderdto.Customer.Phone);
            if (getUser == null)
            {
                getUser = _mapper.Map<Customer>(orderdto.Customer);
                await _customerRepository.Insert(getUser);

            }
            else
            {
                getUser.UpdateCustomerInfo(_mapper.Map<Customer>(orderdto.Customer));

                _customerRepository.Update(getUser);
            }

           
            var order = _mapper.Map<Order>(orderdto);
            order.CustomerId = getUser.Id;

            await _orderRepository.Insert(order);

            foreach (var od in orderdto.orderDetailDtos)
            {
                var realOd = _mapper.Map<OrderDetail>(od);
                if (realOd == null) { return false; }
                realOd.OrderId = order.Id;
                realOd.ProductId = od.ProductOrderDto.Id;
                await _orderdetailsRepository.Insert(realOd);
            }
            

           
            return true;

        }

        public async Task<List<OrderDto>> GetAllCurrentOrder(string phone)
        {
            var orderDtos = new List<OrderDto>();
            var user = await _customerRepository.GetCustomerByPhone(phone);
            if(user == null) return orderDtos;

            var orders = from o in _orderRepository.GetAll()
                         where o.CustomerId == user.Id
                         select o;
            foreach (var od in orders)
            {
                var order = _mapper.Map<OrderDto>(od);
                if (order == null) continue;
                orderDtos.Add(order);
            }

            return orderDtos;
        }

        public async Task<List<OrderDetailDto>> GetDetailsInOrder(Guid Id)
        {
            var orderdetails = await _orderdetailsRepository.GetDetailsInOrder(Id);

            var products = await _productRepository.GetAllList();

            var orderdetaildtos = _mapper.Map<List<OrderDetailDto>>(orderdetails);

            foreach (var orderdetail in orderdetaildtos)
            {
                var product = products.FirstOrDefault(x => x.Id == orderdetail.ProductOrderDto.Id);
                if(product != null)
                    orderdetail.ProductOrderDto = _mapper.Map<ProductOrderDto>(product);
            }

            return orderdetaildtos;
        }

       




        //GetListOfOrder
        //GetOrderByStatus
        //CancelOrder
    }
}
