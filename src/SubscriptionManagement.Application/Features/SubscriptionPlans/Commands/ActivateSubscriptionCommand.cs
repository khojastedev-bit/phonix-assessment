using CSharpFunctionalExtensions;
using MediatR;

namespace SubscriptionManagement.Application.Features.SubscriptionPlans.Commands;
public record ActivateSubscriptionCommand(Guid UserId, Guid PlanId)
    : IRequest<Result<UserSubscriptionDto>>;