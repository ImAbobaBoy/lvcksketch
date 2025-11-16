using System.Text.Json.Serialization;
using Microsoft.AspNetCore.SignalR;

namespace lvcksketch;

public class DrawHub : Hub
{
    public async Task SendStroke(StrokeDto stroke)
    {
        // отправляем всем, кроме отправителя
        await Clients.Others.SendAsync("ReceiveStroke", stroke);
    }
}

public record StringDto(string s1, string s2);
public record StrokeDto(double x1, double y1, double x2, double y2);

