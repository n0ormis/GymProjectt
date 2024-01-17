using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GymProject.Contract.Commands.Payments;

namespace GymProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-session")]
    [Authorize]
    public async Task<string> CreateSessionAsync([FromBody] CreatePaymentCommand command)
        => await _mediator.Send(command);

    [HttpPost("confirm-payment")]
    public async Task ConfirmPaymentAsync([FromBody] ConfirmPaymentCommand command)
        => await _mediator.Send(command);
}