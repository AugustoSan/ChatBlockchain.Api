// Modelo del mensaje cifrado que viaja por WebSocket
namespace ChatBlockchain.Core.Models;
public class EncryptedMessageEnvelope
{
    public required string From { get; set; } = string.Empty;           // Dirección origen (lo asigna el servidor)
    public required string To { get; set; } = string.Empty;             // Dirección destino (en claro para enrutamiento)
    public required string Timestamp { get; set; } = string.Empty;      // ISO 8601
    public required string Text { get; set; } = string.Empty;          // Base64
    public string? AuthTag { get; set; } = string.Empty;        // Base64
    public string? EncryptedKey { get; set; } = string.Empty;   // Base64
    public string? Iv { get; set; } = string.Empty;             // Base64
}