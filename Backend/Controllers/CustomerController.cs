using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.IRepositories;
using Project.Models;
using Project.Repository;

namespace Project.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerService;

        public CustomerController(ICustomerRepository customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomerd()
        {
            var customers = await _customerService.GetAll();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        

        [HttpGet("ByCustomerId/{CustomerId}")]
        public async Task<IActionResult> GetCustomerId([FromRoute] int CustomerId)
        {
            var customer = await _customerService.GetByCustomerId(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> OpenAccount(UserInfoRequest user)
        {
            try
            {
                Customer c = await _customerService.OpenAccount(user.CustomerId, user.initialCredits);
                return Ok(c);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while opening the account: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("user-information")]
        public async Task<IActionResult> GetUserInformation(int customerId)
        {
            var userInformation = await _customerService.GetUserInfo(customerId);

            if (userInformation == null)
            {
                return NotFound();
            }

            return Ok(userInformation);
        }
    }
}
