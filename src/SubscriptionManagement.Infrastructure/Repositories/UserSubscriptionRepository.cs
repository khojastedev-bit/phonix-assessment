using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Core.Entities;
using SubscriptionManagement.Core.Repositories;
using SubscriptionManagement.Core.Utils.Enums;
using SubscriptionManagement.Infrastructure.Data;

namespace SubscriptionManagement.Infrastructure.Repositories;
public class UserSubscriptionRepository : IUserSubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public UserSubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserSubscription?> GetActiveSubscriptionAsync(Guid userId)
    {
        return await _context.UserSubscriptions
            .Include(us => us.SubscriptionPlan)
            .FirstOrDefaultAsync(us =>
                us.UserId == userId &&
                us.Status == SubscriptionStatusEnum.Active &&
                us.EndDate > DateTime.UtcNow);
    }

    public async Task<UserSubscription?> GetByIdAsync(Guid id)
    {
        return await _context.UserSubscriptions
            .FindAsync(id);
    }

    public async Task<UserSubscription?> GetByIdWithPlanAsync(Guid id)
    {
        return await _context.UserSubscriptions
            .Include(us => us.SubscriptionPlan)
            .FirstOrDefaultAsync(us => us.Id == id);
    }

    public async Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(Guid userId)
    {
        return await _context.UserSubscriptions
            .Where(us => us.UserId == userId)
            .OrderByDescending(us => us.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserSubscription>> GetUserSubscriptionsWithPlansAsync(Guid userId)
    {
        return await _context.UserSubscriptions
            .Include(us => us.SubscriptionPlan)
            .Where(us => us.UserId == userId)
            .OrderByDescending(us => us.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserSubscription>> GetExpiredSubscriptionsAsync()
    {
        return await _context.UserSubscriptions
            .Include(us => us.SubscriptionPlan)
            .Where(us => us.Status == SubscriptionStatusEnum.Active &&
                         us.EndDate <= DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task AddAsync(UserSubscription subscription)
    {
        await _context.UserSubscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();
    }

    public void Update(UserSubscription subscription)
    {
        _context.UserSubscriptions.Update(subscription);
        _context.SaveChanges();
    }

    public async Task<bool> ExistsAsync(Guid subscriptionId)
    {
        return await _context.UserSubscriptions
            .AnyAsync(us => us.Id == subscriptionId);
    }

    public async Task<int> GetActiveSubscriptionsCountAsync(Guid planId)
    {
        return await _context.UserSubscriptions
            .CountAsync(us =>
                us.SubscriptionPlanId == planId &&
                us.Status == SubscriptionStatusEnum.Active &&
                us.EndDate > DateTime.UtcNow);
    }
}
