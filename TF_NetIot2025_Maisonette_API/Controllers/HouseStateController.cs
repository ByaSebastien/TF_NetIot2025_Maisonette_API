using Microsoft.AspNetCore.Mvc;
using TF_NetIot2025_Maisonette_API.Entities;
using TF_NetIot2025_Maisonette_API.Entities.Contexts;
using TF_NetIot2025_Maisonette_API.Services;

namespace TF_NetIot2025_Maisonette_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseStateController : ControllerBase
    {

        private readonly MyDbContext _dbContext;
        private readonly MqttService _mqttService;

        public HouseStateController(MyDbContext dbContext, MqttService mqttService)
        {
            _dbContext = dbContext;
            _mqttService = mqttService;
        }

        [HttpGet]
        public IActionResult GetStates()
        {
            HouseState state = _dbContext.HouseStates.SingleOrDefault(hs => hs.Id == 1)!;

            return Ok(state);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLight()
        {
            // Contacter maisonnette pour allumer lampe
            Console.WriteLine("Toggle!!!");

            await _mqttService.PublishAsync("maisonnette/light", "");
            
            return NoContent();
        }
    }
}
