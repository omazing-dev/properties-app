using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.Application.DTOs;
using PropertyApp.Application.Interfaces;

namespace PropertyApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<PropertyDTO>>> GetProperties(
            [FromQuery] string? name,
            [FromQuery] string? address,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _propertyService.GetPropertiesAsync(
                name, address, minPrice, maxPrice, page, pageSize);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
                return NotFound(new { message = "Property not found" });

            return Ok(property);
        }
    }
}
