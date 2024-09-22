using System.Text.Json;
using BusinessObjects.DataTranfer;
using Microsoft.AspNetCore.Mvc;
using Repositories.CustomerRepo;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return Ok(JsonSerializer.Serialize(customers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            var customer = await repository.GetCustomer(id);
            return Ok(JsonSerializer.Serialize(customer));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO customer)
        {
            var _customer = await repository.CheckCustomerExist(customer.Username);
            if (_customer == false)
            {
                await repository.AddCustomer(customer);
                return Created();
            }
            return BadRequest("Customer is exist");
        }

        [HttpPost]
        [Route("MultipleDelete")]
        public async Task<IActionResult> MultipleDelete(int[] selectedIds)
        {
            await repository.MultipleDeleteCustomer(selectedIds);
            return Ok("Delete customer successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDTO customer)
        {
            var _customer = await repository.GetCustomer(id);
            if (_customer != null)
            {
                await repository.UpdateCustomer(id, customer);
                return NoContent();
            }
            return NotFound("Customer is not exist");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            Boolean isDelete = await repository.DeleteCustomer(id);
            if (isDelete == true)
            {
                return Ok("Delete customer successfully");
            }
            return NotFound("Customer is not exist");
        }
    }
}