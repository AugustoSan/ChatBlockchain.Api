using System.Net.WebSockets;
using System.Text;

namespace ChatBlockchain.Api.Services
{
    public class WebSocketManagerService(ILogger<WebSocketManagerService> logger)
    {
        private readonly ILogger<WebSocketManagerService> _logger = logger;
        private readonly Dictionary<string, WebSocket> _sockets = new Dictionary<string, WebSocket>();
        
        public void AddSocket(string address, WebSocket socket)
        {
            // Implement logic to add the WebSocket connection for the given address
            _logger.LogInformation($"WebSocket connection added for address: {address}");
            _sockets.TryAdd(address, socket);
        }

        public void RemoveSocket(string address)
        {
            // Implement logic to remove the WebSocket connection for the given address
            _logger.LogInformation($"WebSocket connection removed for address: {address}");
            _sockets.Remove(address);
        }

        public async Task SendToAddressAsync(string address, string messageJson)
        {
            // Implement logic to send a message to the WebSocket connection associated with the given address
            if (_sockets.TryGetValue(address, out var socket) && socket.State == WebSocketState.Open)
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes(messageJson);
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                _logger.LogInformation($"Message sent to address: {address} - {messageJson}");
            }
            else
            {
                _logger.LogWarning($"WebSocket connection not found or closed for address: {address}");
            }
        }
        public async Task BroadcastToAllExceptAsync(string exceptAddress, string messageJson)
        {
            var tasks = _sockets.Where(kvp => kvp.Key != exceptAddress && kvp.Value.State == WebSocketState.Open)
                                .Select(kvp => kvp.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(messageJson)), WebSocketMessageType.Text, true, CancellationToken.None));
            await Task.WhenAll(tasks);
        }
    }
}