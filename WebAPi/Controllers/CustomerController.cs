using BusinessObjects.DataTranfer;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.CustomerRepo;

namespace WebAPi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository repository;
        public CustomerController(ICustomerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomer()
        {
            var customers = await repository.GetAllCustomers();
            return Ok(customers);
        }
    }
}