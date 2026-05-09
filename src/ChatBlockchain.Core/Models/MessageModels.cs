// Modelo del mensaje cifrado que viaja por WebSocket
namespace ChatBlockchain.Core.Models;
public class EncryptedMessageEnvelope
{
    public string EncryptedKey { get; set; } = string.Empty;   // Base64
    public string Iv { get; set; } = string.Empty;             // Base64
    public string Ciphertext { get; set; } = string.Empty;     // Base64
    public string AuthTag { get; set; } = string.Empty;        // Base64
    public string To { get; set; } = string.Empty;             // Dirección destino (en claro para enrutamiento)
    public string From { get; set; } = string.Empty;           // Dirección origen (lo asigna el servidor)
    public string Timestamp { get; set; } = string.Empty;      // ISO 8601
}