using Microsoft.AspNetCore.Mvc;
using Repositories.CustomerRepo;

namespace WebAPi.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository repository;
        public CustomerController(ICustomerRepository repository)
        {
            this.repository = repository;
        }
    }
}