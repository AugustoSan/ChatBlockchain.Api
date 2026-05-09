using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChatBlockchain.Core.Models;
using ChatBlockchain.Api.Services;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MediatR;
using ChatBlockchain.Application.Users.Queries.GetChallenge;
using ChatBlockchain.Application.Users.Command.RegisterPublicKey;
using ChatBlockchain.Application.Users.Queries.GetValidateAddress;

namespace ChatBlockchain.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IMediator _mediator = mediator;
    [HttpPost("challenge")]
    public async Task<IActionResult> GetChallenge([FromBody] ChallengeRequest req)
    {
        _logger.LogInformation("Solicitando desafío para dirección: {Address}", req.Address);
        var response = await _mediator.Send(new GetChallengeQuery(){ Address = req.Address });
        return Ok(response);
    }

    [Authorize]
    [HttpPost("registerPublicKey")]
    public async Task<IActionResult> RegisterPublicKey([FromBody] RegisterPublicKeyRequest req)
    {
        _logger.LogInformation("Registrando clave pública para dirección: {Address}", req.PublicKeyHex);
        await _mediator.Send(new RegisterPublicKeyCommand { PublicKey = req.PublicKeyHex });
        return Ok();
    }

    [Authorize]
    [HttpGet("validate/{address}")]
    public async Task<IActionResult> GetValidateAddress(string address)
    {
        _logger.LogInformation("Obteniendo clave pública para dirección: {Address}", address);
        var response = await _mediator.Send(new GetValidateAddressQuery { Address = address });   
        return Ok(response);
    }
}