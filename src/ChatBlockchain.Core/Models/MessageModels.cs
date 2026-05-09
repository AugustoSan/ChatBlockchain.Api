// Modelo del mensaje cifrado que viaja por WebSocket
using System.Text.Json.Serialization;

namespace ChatBlockchain.Core.Models;
public class EncryptedMessageEnvelope
{
    [JsonPropertyName("from")]
    public required string From { get; set; } = string.Empty;           // Dirección origen (lo asigna el servidor)
    [JsonPropertyName("to")]
    public required string To { get; set; } = string.Empty;             // Dirección destino (en claro para enrutamiento)
    [JsonPropertyName("timestamp")]
    public required string Timestamp { get; set; } = string.Empty;      // ISO 8601
    [JsonPropertyName("text")]
    public required string Text { get; set; } = string.Empty;          // Base64
    [JsonPropertyName("authTag")]
    public string? AuthTag { get; set; } = string.Empty;        // Base64
    [JsonPropertyName("encryptedKey")]
    public string? EncryptedKey { get; set; } = string.Empty;   // Base64
    [JsonPropertyName("iv")]
    public string? Iv { get; set; } = string.Empty;             // Base64
}