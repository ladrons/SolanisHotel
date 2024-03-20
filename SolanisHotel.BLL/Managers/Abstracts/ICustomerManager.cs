using SolanisHotel.BLL.DTOs;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Abstracts
{
    public interface ICustomerManager : IManager<Customer>
    {
        Task<Customer> AuthenticateCustomer(string email, string password);

        Task<Customer> GetOrCreateCustomer(string email);


        CustomerDTO MappingToCustomerDTO(Customer customer);


        //TEST AREA

        Task<bool> RegisterCustomer(CustomerDTO customerDTO);




        Task<bool> RegisterOrUpdateCustomer(CustomerDTO customerDTO);


        Task<CustomerDTO> UpdateCustomerIfGuest(CustomerDTO customerInfo, int customerId);
    }
}