using lvcksketch;
using lvcksketch.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.ConfigureUseCases();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// Статика и фронт должны быть ДО контроллеров
app.UseDefaultFiles();
app.UseStaticFiles();

// Контроллеры
app.MapControllers();

// SignalR
app.MapHub<DrawHub>("/draw");

app.Run();

