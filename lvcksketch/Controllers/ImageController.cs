using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace lvcksketch.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET api/image?name=Hellas.png
        [HttpGet]
        public IActionResult GetImage([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Имя файла не указано");
            }

            // формируем полный путь к файлу в wwwroot/images/
            var filePath = Path.Combine(_env.WebRootPath, name);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Файл не найден");
            }

            // определяем MIME тип по расширению
            var contentType = name.EndsWith(".png") ? "image/png" :
                name.EndsWith(".jpg") || name.EndsWith(".jpeg") ? "image/jpeg" :
                "application/octet-stream";

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, contentType);
        }
    }
}