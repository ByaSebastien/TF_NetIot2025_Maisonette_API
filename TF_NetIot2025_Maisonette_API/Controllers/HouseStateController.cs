using Microsoft.AspNetCore.Mvc;
using TF_NetIot2025_Maisonette_API.Entities;
using TF_NetIot2025_Maisonette_API.Entities.Contexts;

namespace TF_NetIot2025_Maisonette_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseStateController : ControllerBase
    {

        private readonly MyDbContext _dbContext;

        public HouseStateController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetStates()
        {
            HouseState state = _dbContext.HouseStates.SingleOrDefault(hs => hs.Id == 1)!;

            return Ok(state);
        }

        [HttpPost]
        public IActionResult ToggleLight()
        {
            // Contacter maisonnette pour allumer lampe
            Console.WriteLine("Toggle!!!");
            
            return NoContent();
        }
    }
}
