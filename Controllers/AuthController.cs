using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChatBlockchain.Api.Models;
using ChatBlockchain.Api.Services;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace ChatBlockchain.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private static readonly ConcurrentDictionary<string, (string nonce, string challenge, DateTime expiry)> _nonces = new();
    private static readonly ConcurrentDictionary<string, string> _publicKeys = new(); // address -> publicKeyHex
    private readonly ILogger<AuthController> _logger;

    private readonly CryptoService _crypto;
    private readonly JwtService _jwt;

    public AuthController(CryptoService crypto, JwtService jwt, ILogger<AuthController> logger)
    {
        _crypto = crypto;
        _jwt = jwt;
        _logger = logger;
    }

    [HttpPost("challenge")]
    public IActionResult GetChallenge([FromBody] ChallengeRequest req)
    {
        _logger.LogInformation("Solicitando desafío para dirección: {Address}", req.Address);
        var nonce = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        var challengeMsg = $"Inicia sesión en ChatBlockchain con nonce: {nonce}";
        _nonces[req.Address] = (nonce, challengeMsg, DateTime.UtcNow.AddMinutes(5));
        return Ok(new ChallengeResponse { Challenge = challengeMsg });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        _logger.LogInformation("Intentando iniciar sesión para dirección: {Address} {Challenge} {Signature}", req.Address, req.OriginalChallenge, req.Signature);
        _logger.LogInformation("Nonces actuales: {Nonces}", !_nonces.TryGetValue(req.Address, out var store) ? "No encontrado" : $"Encontrado - Challenge: {store.challenge}, Expiry: {store.expiry}");
        if (!_nonces.TryGetValue(req.Address, out var stored) || stored.challenge != req.OriginalChallenge || stored.expiry < DateTime.UtcNow)
            return Unauthorized("Nonce inválido o expirado");

        _logger.LogInformation("Verificando firma para dirección: {Address}", req.Address);
        if (!_crypto.VerifySignature(req.OriginalChallenge, req.Signature, req.Address))
            return Unauthorized("Firma inválida");

        _nonces.TryRemove(req.Address, out _);
        var token = _jwt.GenerateToken(req.Address);
        return Ok(new LoginResponse { Token = token, Address = req.Address });
    }

    [Authorize]
    [HttpPost("registerPublicKey")]
    public IActionResult RegisterPublicKey([FromBody] RegisterPublicKeyRequest req)
    {
        _logger.LogInformation("Registrando clave pública para dirección: {Address}", req.PublicKeyHex);
        var address = User.FindFirst("address")?.Value;
        if (string.IsNullOrEmpty(address)) return Unauthorized();

        _publicKeys[address] = req.PublicKeyHex;
        _logger.LogInformation("Clave pública registrada para dirección: {Address}", _publicKeys[address]); return Ok();
    }

    [Authorize]
    [HttpGet("publicKey/{address}")]
    public IActionResult GetPublicKey(string address)
    {
        _logger.LogInformation("Obteniendo clave pública para dirección: {Address}", address);
        _logger.LogInformation("Claves públicas actuales: {PublicKeys}", _publicKeys.Count == 0 ? "No hay claves registradas" : string.Join(", ", _publicKeys.Select(kv => $"{kv.Key}: {kv.Value}")));
        if (_publicKeys.TryGetValue(address, out var pubKey))
            return Ok(new { publicKey = pubKey });
        return NotFound("Usuario no encontrado");
    }
}