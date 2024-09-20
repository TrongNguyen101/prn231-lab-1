using BusinessObjects.DataTranfer;
using BusinessObjects.Models;

namespace Repositories.CustomerRepo
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDTO>> GetAllCustomers();
    }
}