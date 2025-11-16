using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace lvcksketch;

public class DrawHub : Hub
{
    private static readonly Dictionary<string, string> _currentStrategies = new();
    
    public async Task SendPing(PingDto ping)
    {
        await Clients.Others.SendAsync("ReceivePing", ping);
    }
}

public record PingDto(double x, double y, string color);

