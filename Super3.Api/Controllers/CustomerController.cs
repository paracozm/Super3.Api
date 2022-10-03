using DocumentValidator;
using Microsoft.AspNetCore.Mvc;
using Super3.Application.DataContract.Request.Customer;
using Super3.Application.Interfaces;
using Super3.Domain.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Super3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;

        public CustomerController(ICustomerApplication customerApplication)
        {
            _customerApplication = customerApplication;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await _customerApplication.GetAllAsync();
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            //var customerId = int.Parse(Id);
            var response = await _customerApplication.GetByIdAsync(Id);
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CreateCustomerRequest request)
        {
            
            var response = await _customerApplication.CreateAsync(request);
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);
            
            return Ok(response + " New customer has been created"); //////////////////////////////////////// NAO VOLTA RESPONSE BODY
        }






        /*[HttpPut("{Id}")]/////errror
        public async Task<ActionResult> Put(int Id, [FromBody]UpdateCustomerRequest request)
        {
            var response = await _customerApplication.UpdateAsync(request);
            if (response.Report.Any())
                return UnprocessableEntity(response.Report); 

            return Ok(response + " Customer has been updated");
        }*/
    }
}
