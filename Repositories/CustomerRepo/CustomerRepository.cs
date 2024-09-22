using BusinessObjects.DataTranfer;
using BusinessObjects.Models;
using DataAccess.CustomerDataAccess;

namespace Repositories.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<CustomerDTO> MapToListDto(List<Customer> customer)
        {
            return customer.Select(x => new CustomerDTO
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                Fullname = x.Fullname,
                Gender = x.Gender,
                Birthday = x.Birthday,
                Address = x.Address,
            }).ToList();
        }
        public CustomerDTO MapToDto(Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                Username = customer.Username,
                Password = customer.Password,
                Fullname = customer.Fullname,
                Gender = customer.Gender,
                Birthday = customer.Birthday,
                Address = customer.Address,
            };
        }
        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            var customers = await CustomerDataAccess.GetInstance().GetCustomers();
            return MapToListDto(customers);
        }

        public async Task<CustomerDTO> GetCustomer(int id)
        {
            var customer = await CustomerDataAccess.GetInstance().GetCustomerById(id);
            return MapToDto(customer);
        }

        public async Task AddCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new Customer
            {
                Username = customerDTO.Username,
                Password = customerDTO.Password,
                Fullname = customerDTO.Fullname,
                Gender = customerDTO.Gender,
                Birthday = customerDTO.Birthday,
                Address = customerDTO.Address,
            };
            await CustomerDataAccess.GetInstance().AddCustomer(customer);
        }

        public async Task<Boolean> CheckCustomerExist(string username)
        {
            var customer = await CustomerDataAccess.GetInstance().GetCustomerByUsername(username);
            if (customer != null)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateCustomer(int id, CustomerDTO customerDTO)
        {
            var customer = await CustomerDataAccess.GetInstance().GetCustomerById(id);
            customer.Id = id;
            customer.Username = customerDTO.Username;
            customer.Password = customerDTO.Password;
            customer.Fullname = customerDTO.Fullname;
            customer.Gender = customerDTO.Gender;
            customer.Birthday = customerDTO.Birthday;
            customer.Address = customerDTO.Address;
            await CustomerDataAccess.GetInstance().UpdateCustomer(customer);
        }

        public async Task<Boolean> DeleteCustomer(int id)
        {
            var customer = await CustomerDataAccess.GetInstance().GetCustomerById(id);
            if (customer != null)
            {
                await CustomerDataAccess.GetInstance().DeleteCustomer(customer);
                return true;
            }
            return false;
        }
        
        public async Task MultipleDeleteCustomer(int[] selectedIds)
        {
            foreach (int id in selectedIds)
            {
                var customer = await CustomerDataAccess.GetInstance().GetCustomerById(id);
                if (customer != null)
                {
                    await CustomerDataAccess.GetInstance().DeleteCustomer(customer);
                }
            }
        }
    }
}