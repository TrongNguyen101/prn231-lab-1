using BusinessObjects.Models;
using DataAccess.CustomerDataAccess;

namespace Repositories.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<List<Customer>> GetAllCustomers() => await CustomerDataAccess.GetInstance().GetCustomers();
    }
}