using ExceptionLogsTask.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ExceptionLogsTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly DatabaseLogger _databaseLogger;

        // Constructor to inject both the ILogger and DatabaseLogger
        public ProductController(ILogger<ProductController> logger, DatabaseLogger databaseLogger)
        {
            _logger = logger;
            _databaseLogger = databaseLogger;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            try
            {
                // Simulating a scenario that could throw an exception
                throw new InvalidOperationException("An error occurred while fetching products.");
            }
            catch (Exception ex)
            {
                // Log the exception using the standard logger
                _logger.LogError(ex, "Error in GetAllProducts endpoint");

                // Log the exception directly to the database
                _databaseLogger.LogError("ProductController", "GetAllProducts", ex);

                return StatusCode(500, "Internal server error occurred.");
            }
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromBody] string product)
        {
            try
            {
                // Simulating a scenario that could throw an exception
                if (string.IsNullOrEmpty(product))
                {
                    throw new ArgumentNullException(nameof(product), "Product name is required.");
                }

                // Logic to create a product would go here

                return Ok(new { message = "Product created successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception using the standard logger
                _logger.LogError(ex, "Error in CreateProduct endpoint");

                // Log the exception directly to the database
                _databaseLogger.LogError("ProductController", "CreateProduct", ex);

                return StatusCode(500, "Internal server error occurred.");
            }
        }
    }
}
