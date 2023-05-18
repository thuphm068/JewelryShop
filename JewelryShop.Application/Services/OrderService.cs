using JewelryShop.Application.Contracts;
using JewelryShop.Domain.Entities;
using JewelryShop.Domain.Repository;
using MapsterMapper;


namespace JewelryShop.Application.Services
{
    public class OrderService
    {

        //set up related repository using Dependency Injection
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SubCategory> _subcategoryRepository;
        private readonly IRepository<Warranty> _warrantyRepository;
        private readonly IRepository<Customer> _customerRepository;
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
            IRepository<Customer> customerRepository,
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
