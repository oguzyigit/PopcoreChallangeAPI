using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PopcoreChallangeAPI.Extension;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopcoreChallangeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _service;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        public IActionResult GetByParameter([FromQuery] string ingredient, [FromQuery] int limit)
        {
            if (limit > 20)
                return BadRequest("It's not allowed to request more than 20 products per request");

            if (limit <= 0)
                return BadRequest("Please request at least 1 product");

            try
            {
                IRestClient client = new RestClient("https://world.openfoodfacts.org/cgi/");

                List<Product> products = _service.GetProductByIngredient(ingredient, limit).Result;

                return Ok(products);
            }
            catch
            {
                return StatusCode(400, "There was an error on getting data");
            }
        }
    }
}
