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

    public async Task<CustomerDto?> ManageAccount(string phone )
    {
        var user = await _customerRepository.GetCustomerByPhone(phone);
        if (user == null) { return null; }

        return _mapper.Map<CustomerDto>(user);


    }  
    public async Task<bool> EditAccount(CustomerDto customerDto)
    {

        var user = await _customerRepository.GetById(customerDto.Id);
        if (user == null) { return false; }

        user.Phone = customerDto.Phone;
        user.Name = customerDto.Name;
        user.Email = customerDto.Email;
        user.Address = customerDto.Address;
        user.Birthday = customerDto.Birthday;
        user.Gender = customerDto.Gender;
        try
        {
            _customerRepository.Update(user);
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }


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
