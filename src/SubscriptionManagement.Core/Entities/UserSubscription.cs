using SubscriptionManagement.Core.Utils.Enums;

namespace SubscriptionManagement.Core.Entities;
public class UserSubscription
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid SubscriptionPlanId { get; private set; }
    public SubscriptionPlan SubscriptionPlan { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime? ActualEndDate { get; private set; }
    public SubscriptionStatusEnum Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private UserSubscription() { }

    public UserSubscription(Guid userId, Guid subscriptionPlanId, SubscriptionPlan plan)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        SubscriptionPlanId = subscriptionPlanId;
        SubscriptionPlan = plan;
        StartDate = DateTime.UtcNow;
        EndDate = StartDate.AddDays(plan.DurationInDays);
        Status = SubscriptionStatusEnum.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == SubscriptionStatusEnum.Active)
        {
            Status = SubscriptionStatusEnum.Cancelled;
            ActualEndDate = DateTime.UtcNow;
        }
    }

    public void Expire()
    {
        Status = SubscriptionStatusEnum.Expired;
        ActualEndDate = DateTime.UtcNow;
    }

    public bool IsActive => Status == SubscriptionStatusEnum.Active &&
                            DateTime.UtcNow <= EndDate;
}
