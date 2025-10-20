namespace SubscriptionManagement.Application.Features.SubscriptionPlans.Commands;
public record UserSubscriptionDto(
    Guid Id,
    Guid UserId,
    Guid SubscriptionPlanId,
    string PlanName,
    DateTime StartDate,
    DateTime EndDate,
    string Status,
    bool IsActive,
    int DaysRemaining
);

