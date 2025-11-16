using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace lvcksketch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StrategyController : ControllerBase
{
    private readonly string _folder;

    public StrategyController(IWebHostEnvironment env)
    {
        _folder = Path.Combine(env.WebRootPath, "strats");
        if (!Directory.Exists(_folder))
            Directory.CreateDirectory(_folder);
    }

    // GET api/strategy -> возвращает список всех стратегий
    [HttpGet]
    public IActionResult GetAll()
    {
        var files = Directory.GetFiles(_folder, "*.json")
            .Select(f => Path.GetFileNameWithoutExtension(f))
            .ToList();

        return Ok(files);
    }

    // GET api/strategy/load?name=MyStrat -> возвращает JSON конкретной стратегии
    [HttpGet("load")]
    public IActionResult Load([FromQuery] string name)
    {
        var filePath = Path.Combine(_folder, $"{name}.json");
        if (!System.IO.File.Exists(filePath))
            return NotFound("Стратегия не найдена");

        var json = System.IO.File.ReadAllText(filePath);
        return Content(json, "application/json");
    }

    // // POST api/strategy/save -> сохраняет стратегию
    // [HttpPost("save")]
    // public async Task<IActionResult> Save([FromBody] StrategyDto strategy)
    // {
    //     if (strategy == null || string.IsNullOrWhiteSpace(strategy.StrategyName))
    //         return BadRequest("Неверные данные");
    //
    //     var filePath = Path.Combine(_folder, $"{strategy.StrategyName}.json");
    //     var json = JsonSerializer.Serialize(strategy, new JsonSerializerOptions { WriteIndented = true });
    //     await System.IO.File.WriteAllTextAsync(filePath, json);
    //
    //     return Ok();
    // }
}