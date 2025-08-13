using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyApp.Infraestructure.Context;

namespace PropertyApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseTestsController : ControllerBase
    {
        private readonly MongoDbContext _context;
        public DatabaseTestsController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("init")]
        public IActionResult Initialize()
        {
            try
            {
                _context.InitializeDatabase();
                return Ok("Base de datos inicializada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inicializando la base de datos: {ex.Message}");
            }
        }
    }
}
