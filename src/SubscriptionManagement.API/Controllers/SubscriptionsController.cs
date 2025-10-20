using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubscriptionManagement.Application.Features.SubscriptionPlans.Commands;
using SubscriptionManagement.Application.Features.SubscriptionPlans.Queries;

namespace SubscriptionManagement.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("plans")]
    public async Task<IActionResult> GetActivePlans()
    {
        var result = await _mediator.Send(new GetAllActivePlansQuery());
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("activate")]
    public async Task<IActionResult> ActivateSubscription(ActivateSubscriptionRequest request)
    {
        var command = new ActivateSubscriptionCommand(request.UserId, request.PlanId);
        var result = await _mediator.Send(command);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    [HttpPost("{subscriptionId:guid}/cancel")]
    public async Task<IActionResult> CancelSubscription(Guid subscriptionId)
    {
        //todo: 
        throw new NotImplementedException();
    }

    [HttpGet("users/{userId:guid}/active")]
    public async Task<IActionResult> GetActiveSubscription(Guid userId)
    {
        //todo: 
        throw new NotImplementedException();
    }
}

public record ActivateSubscriptionRequest(Guid UserId, Guid PlanId);
