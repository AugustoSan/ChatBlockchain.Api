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
            string normalizedAddress = address.ToUpper();
            // Implement logic to add the WebSocket connection for the given address
            _logger.LogInformation($"WebSocket connection added for address: {normalizedAddress}");
            _sockets.TryAdd(normalizedAddress, socket);
        }

        public void RemoveSocket(string address)
        {
            string normalizedAddress = address.ToUpper();
            // Implement logic to remove the WebSocket connection for the given address
            _logger.LogInformation($"WebSocket connection removed for address: {normalizedAddress}");
            _sockets.Remove(normalizedAddress);
        }

        public async Task SendToAddressAsync(string address, string messageJson)
        {
            // Implement logic to send a message to the WebSocket connection associated with the given address
            string normalizedAddress = address.ToUpper();
            _sockets.TryGetValue(normalizedAddress, out var socket);
            _logger.LogInformation($"Enviando mensaje a la dirección: {normalizedAddress}");
            _logger.LogInformation($"Socket encontrado: {socket != null}");
            string text = "Socket no encontrado";
            _logger.LogInformation($"Estado del socket{normalizedAddress}: {((socket != null) ? socket.State : text )}");
            if (socket != null && socket.State == WebSocketState.Open)
            {
                var buffer = System.Text.Encoding.UTF8.GetBytes(messageJson);
                await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                _logger.LogInformation($"Message sent to address: {normalizedAddress} - {messageJson}");
            }
            else
            {
                _logger.LogWarning($"WebSocket connection not found or closed for address: {normalizedAddress}");
            }
        }
        public async Task BroadcastToAllExceptAsync(string exceptAddress, string messageJson)
        {
            string normalizedExceptAddress = exceptAddress.ToUpper();
            var tasks = _sockets.Where(kvp => kvp.Key != normalizedExceptAddress && kvp.Value.State == WebSocketState.Open)
                                .Select(kvp => kvp.Value.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(messageJson)), WebSocketMessageType.Text, true, CancellationToken.None));
            await Task.WhenAll(tasks);
        }
    }
}