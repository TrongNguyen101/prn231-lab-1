using BusinessObjects.DataTranfer;
using BusinessObjects.Models;

namespace Repositories.CustomerRepo
{
    public interface ICustomerRepository
    {
        Task<Boolean> CheckCustomerExist(string username);
        Task<List<CustomerDTO>> GetAllCustomers();
        Task<CustomerDTO> GetCustomer(int id);
        Task AddCustomer(CustomerDTO customerDTO);
        Task UpdateCustomer(int id, CustomerDTO customerDTO);
        Task<Boolean> DeleteCustomer(int id);
    }
}