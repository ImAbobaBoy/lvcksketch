using lvcksketch.UseCases.GetMapNames.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lvcksketch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController : ControllerBase
    {
        private readonly IGetMapNames _getMapNames;

        public MapController(IGetMapNames getMapNames)
        {
            _getMapNames = getMapNames;
        }

        // GET api/map
        [HttpGet]
        public async Task<IActionResult> GetMaps()
        {
            var maps = await _getMapNames.ExecuteAsync();
            return Ok(maps);
        }
    }
}