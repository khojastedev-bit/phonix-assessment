using SubscriptionManagement.Core.Entities;

namespace SubscriptionManagement.Core.Repositories;
public interface IUserSubscriptionRepository
{
    Task<UserSubscription?> GetActiveSubscriptionAsync(Guid userId);
    Task<UserSubscription?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(Guid userId);
    Task AddAsync(UserSubscription subscription);
    void Update(UserSubscription subscription);
}