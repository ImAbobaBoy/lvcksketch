using lvcksketch;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseDefaultFiles(); // чтобы открывался index.html
app.UseStaticFiles();
app.MapHub<DrawHub>("/draw");

app.Run();
