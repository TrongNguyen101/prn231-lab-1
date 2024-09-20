using BusinessObjects.DataTranfer;
using BusinessObjects.Models;
using DataAccess.CustomerDataAccess;

namespace Repositories.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<CustomerDTO> MapToDTO(List<Customer> customer)
        {
            return customer.Select(x => new CustomerDTO
            {
                Username = x.Username,
                Password = x.Password,
                Fullname = x.Fullname,
                Birthday = x.Birthday,
                Address = x.Address,
            }).ToList();
        }
        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            var customers = await CustomerDataAccess.GetInstance().GetCustomers();
            return MapToDTO(customers);
        }
    }
}