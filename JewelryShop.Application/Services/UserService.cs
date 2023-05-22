using JewelryShop.Application.Contracts;
using JewelryShop.Domain.Entities;
using JewelryShop.Domain.Repository;
using MapsterMapper;

namespace JewelryShop.Application.Services;

public class UserService : IUserService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UserService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<bool> Login(CustomerDto customerDto)
    {
        var user = await _customerRepository.GetCustomerByPhone(customerDto.Phone);
        if (user == null) { return false; }
        if (user.Password == customerDto.Password && user.Phone == customerDto.Phone)
        {
            customerDto.Email = user.Email;
            customerDto.Name = user.Name;

            return true;
        }
        return false;
    }

    public async Task<CustomerDto?> ManageAccount(CustomerDto customerDto)
    {
        var user = await _customerRepository.GetCustomerByPhone(customerDto.Phone);
        if (user == null) { return null; }
        if (user.Password == customerDto.Password && user.Phone == customerDto.Phone)
        {
            return _mapper.Map<CustomerDto>(user);
        }
        return null;
    }

    public async Task<bool> Register(CustomerDto customerDto)
    {
        try
        {
            var user = _mapper.Map<Customer>(customerDto);
            await _customerRepository.Insert(user);

            return true;
        }

        catch (Exception ex)
        {
            return false;
        }

    }

    
}
