using AutoMapper;
using SolanisHotel.BLL.DTOs;
using SolanisHotel.BLL.Managers.Abstracts;
using SolanisHotel.DAL.Repositories.Abstracts;
using SolanisHotel.ENTITIES.Models;

namespace SolanisHotel.BLL.Managers.Concretes
{
    public class CustomerManager : BaseManager<Customer>, ICustomerManager
    {
        readonly IMapper _mapper;

        readonly ICustomerRepository _cRep;

        public CustomerManager(ICustomerRepository cRep, IMapper mapper) : base(cRep)
        {
            _cRep = cRep;
            _mapper = mapper;
        }

        //----------//----------//

        public async Task<Customer> AuthenticateCustomer(string email, string password)
        {
            return await _cRep.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
        }

        public async Task<Customer> GetOrCreateCustomer(string email)
        {
            Customer? currentCustomer = await FindCustomerByEmail(email);
            if (currentCustomer != null)
            {
                return currentCustomer;
            }
            else
            {
                Customer newGuest = new Customer
                {
                    Role = ENTITIES.Enums.UserRole.Guest,
                };
                await _cRep.AddAsync(newGuest);
                await _cRep.SaveChangesAsync();

                return newGuest;
            }
        }

        public async Task<CustomerDTO> UpdateCustomerIfGuest(CustomerDTO customerInfo, int customerId)
        {
            Customer currentGuest = await _cRep.FindAsync(customerId);
            if (currentGuest.Role == ENTITIES.Enums.UserRole.Guest)
            {
                currentGuest.FirstName = customerInfo.FirstName;
                currentGuest.LastName = customerInfo.LastName;

                currentGuest.Email = customerInfo.Email;

                currentGuest.PhoneNumber = PhoneNumberFix(customerInfo.PhoneNumber);
                currentGuest.BirthDate = customerInfo.BirthDate;

                await _cRep.Update(currentGuest);
                await _cRep.SaveChangesAsync();

                return MappingToCustomerDTO(currentGuest);
            }
            return MappingToCustomerDTO(currentGuest);
        }

        //----------//

        public async Task<bool> RegisterOrUpdateCustomer(CustomerDTO customerDTO)
        {
            if (await EmailAlreadyExists(customerDTO.Email))
            {
                //E-Posta veri tabanında varsa;
                Customer foundCustomer = await FindCustomerByEmail(customerDTO.Email);

                if (foundCustomer.Role == ENTITIES.Enums.UserRole.Guest)
                {
                    foundCustomer.Password = customerDTO.Password;
                    foundCustomer.Role = ENTITIES.Enums.UserRole.Customer;

                    await _cRep.Update(foundCustomer);
                    await _cRep.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            else
            {
                Customer newCustomer = MappingToCustomer(customerDTO);
                newCustomer.Role = ENTITIES.Enums.UserRole.Customer;

                await _cRep.AddAsync(newCustomer);
                await _cRep.SaveChangesAsync();

                return true;
            }
        }

        //----------//

        public Customer MappingToCustomer(CustomerDTO customerDTO) => _mapper.Map<Customer>(customerDTO);

        public CustomerDTO MappingToCustomerDTO(Customer customer) => _mapper.Map<CustomerDTO>(customer);


        //-----Privates Methods-----//

        private async Task<Customer> FindCustomerByEmail(string email)
        {
            return await _cRep.FirstOrDefaultAsync(c => c.Email == email);
        }

        private async Task<bool> EmailAlreadyExists(string email) => await _cRep.AnyAsync(c => c.Email == email) ? true : false;

        private string PhoneNumberFix(string phoneNumber) => phoneNumber.Replace(" ", "");

        //-----Privates Methods-----//





        //-----Test Area-----//

        //-----Test Area-----//
    }
}