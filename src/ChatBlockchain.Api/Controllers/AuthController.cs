using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChatBlockchain.Api.Services;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using MediatR;
using ChatBlockchain.Core.Models;
using ChatBlockchain.Application.Auth.Command.Login;

namespace ChatBlockchain.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var response = await _mediator.Send(new AuthLoginCommand { Address = req.Address, OriginalChallenge = req.OriginalChallenge, Signature = req.Signature });
        return Ok(response);
    }
}