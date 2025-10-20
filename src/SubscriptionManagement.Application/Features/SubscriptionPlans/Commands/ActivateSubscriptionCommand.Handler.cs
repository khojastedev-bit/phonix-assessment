using CSharpFunctionalExtensions;
using MediatR;
using SubscriptionManagement.Core.Entities;
using SubscriptionManagement.Core.Repositories;

namespace SubscriptionManagement.Application.Features.SubscriptionPlans.Commands;
public class ActivateSubscriptionCommandHandler : IRequestHandler<ActivateSubscriptionCommand,
    Result<UserSubscriptionDto>>
{
    private readonly IUserSubscriptionRepository _userSubscriptionRepository;
    private readonly ISubscriptionPlanRepository _planRepository;

    public ActivateSubscriptionCommandHandler(
        IUserSubscriptionRepository userSubscriptionRepository,
        ISubscriptionPlanRepository planRepository)
    {
        _userSubscriptionRepository = userSubscriptionRepository;
        _planRepository = planRepository;
    }

    public async Task<Result<UserSubscriptionDto>> Handle(
        ActivateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var plan = await _planRepository.GetByIdAsync(request.PlanId);
        if (plan == null || !plan.IsActive)
            return Result.Failure<UserSubscriptionDto>("Subscription plan not found or inactive");

        var activeSubscription = await _userSubscriptionRepository
            .GetActiveSubscriptionAsync(request.UserId);

        if (activeSubscription != null)
            return Result.Failure<UserSubscriptionDto>("User already has an active subscription");

        var subscription = new UserSubscription(request.UserId, request.PlanId, plan);
        await _userSubscriptionRepository.AddAsync(subscription);

        return Result.Success<UserSubscriptionDto>(new UserSubscriptionDto(
            subscription.Id,
            subscription.UserId,
            subscription.SubscriptionPlanId,
            $"{subscription.SubscriptionPlan.Name}_{subscription.SubscriptionPlanId}",
            subscription.StartDate,
            subscription.EndDate,
            subscription.Status.ToString(),
            subscription.IsActive,
            subscription.SubscriptionPlan.DurationInDays
        ));
    }
}

