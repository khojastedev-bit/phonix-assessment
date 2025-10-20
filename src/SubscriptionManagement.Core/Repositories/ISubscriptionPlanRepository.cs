using SubscriptionManagement.Core.Entities;

namespace SubscriptionManagement.Core.Repositories;
public interface ISubscriptionPlanRepository
{
    Task<SubscriptionPlan?> GetByIdAsync(Guid id);
    Task<IEnumerable<SubscriptionPlan>> GetAllActiveAsync();
    Task AddAsync(SubscriptionPlan plan);
    void Update(SubscriptionPlan plan);
}