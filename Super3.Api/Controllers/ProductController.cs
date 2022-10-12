using Microsoft.AspNetCore.Mvc;
using Super3.Application.Applications;
using Super3.Application.DataContract.Request.Customer;
using Super3.Application.DataContract.Request.Product;
using Super3.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Super3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await _productApplication.GetAllAsync();
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // GET api/<ProductController>/5
        [HttpGet("{Id}")]
        public async Task<ActionResult> Get(int Id)
        {
            //var customerId = int.Parse(Id);
            var response = await _productApplication.GetByIdAsync(Id);
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateProductRequest request)
        {
            var response = await _productApplication.CreateAsync(request);
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response + " New product has been created");
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateProductRequest request)
        {
            var response = await _productApplication.UpdateAsync(request);
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response + " Product has been updated");
        }

    }
}
