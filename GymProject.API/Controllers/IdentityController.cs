using GymProject.Contract.Commands.Auth;
using GymProject.Contract.Dtos.Auth;
using GymProject.Contract.Queries.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;
    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sign-in")]
    public async Task<TokenDTO> SignInAsync([FromBody] SignInCommand command)
        => await _mediator.Send(command);

    [HttpPost("refresh-token")]
    public async Task<TokenDTO> RefreshAsync([FromBody] RefreshTokenCommand command)
        => await _mediator.Send(command);

    [HttpPost("sign-up")]
    public async Task SignUpAsync([FromBody] SignUpCommand command)
        => await _mediator.Send(command);

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmMail([FromQuery] ConfirmMailCommand command)
        => Redirect(await _mediator.Send(command));

    [HttpGet("get")]
    public async Task<ProfileDto> GetMe([FromQuery] GetProfileQuery query)
        => await _mediator.Send(query);
}