using Microsoft.AspNetCore.Mvc;
using Super3.Application.DataContract.Request.Order;
using Super3.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Super3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderApplication _orderApplication;

        public OrderController(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await _orderApplication.GetAllAsync();
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var response = await _orderApplication.GetByIdAsync(id);
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOrderRequest request)
        {
            var response = await _orderApplication.CreateAsync(request);
            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

    }
}
