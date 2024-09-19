using BusinessObjects.Models;

namespace Repositories.CustomerRepo
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
    }
}