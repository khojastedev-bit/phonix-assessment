namespace SubscriptionManagement.Application.Features.SubscriptionPlans.Queries;
public record SubscriptionPlanDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int DurationInDays,
    int MaxUsers,
    List<string> Features
);
