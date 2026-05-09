using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using ChatBlockchain.Api.Services;
using ChatBlockchain.Core.Interfaces.Services;
using ChatBlockchain.Core.Models;

namespace ChatBlockchain.Api.Middleware;

public class WebSocketMiddleware(RequestDelegate next, WebSocketManagerService wsManager, IJwtService jwtService, ILogger<WebSocketMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly WebSocketManagerService _wsManager = wsManager;
    private readonly IJwtService _jwtService = jwtService;
    private readonly ILogger<WebSocketMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest && context.Request.Path == "/ws")
        {
            // Obtener token del query string
            var token = context.Request.Query["token"].ToString();
            if (string.IsNullOrEmpty(token) || !_jwtService.ValidateToken(token, out var address))
            {
                context.Response.StatusCode = 401;
                return;
            }

            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            _wsManager.AddSocket(address!, webSocket);
            await HandleWebSocketAsync(address!, webSocket);
        }
        else
        {
            await _next(context);
        }
    }

    private async Task HandleWebSocketAsync(string address, WebSocket webSocket)
    {
        var buffer = new byte[4096];
        try
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }

                var messageJson = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var envelope = JsonSerializer.Deserialize<EncryptedMessageEnvelope>(messageJson);
                if (envelope != null)
                {
                    envelope.From = address;
                    envelope.Timestamp = DateTime.UtcNow.ToString("o");
                    var forwardJson = JsonSerializer.Serialize(envelope);

                    // Enviar solo al destinatario (envío dirigido)
                    await _wsManager.SendToAddressAsync(envelope.To, forwardJson);
                }
            }
        }
        finally
        {
            _wsManager.RemoveSocket(address);
            
            // Solo intentar cerrar si el estado es válido para CloseAsync
            if (webSocket.State == WebSocketState.Open || 
                webSocket.State == WebSocketState.CloseReceived || 
                webSocket.State == WebSocketState.CloseSent)
            {
                try
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
                catch (WebSocketException ex)
                {
                    _logger.LogWarning(ex, "Error occurred while closing WebSocket for address: {Address}", address);
                    // Log si quieres, pero no es necesario relanzar
                    // Console.WriteLine($"Error closing WebSocket: {ex.Message}");
                }
            }
            
            webSocket.Dispose();
}
    }
}