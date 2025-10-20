using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Core.Entities;
using SubscriptionManagement.Core.Repositories;
using SubscriptionManagement.Infrastructure.Data;

namespace SubscriptionManagement.Infrastructure.Repositories;
public class SubscriptionPlanRepository : ISubscriptionPlanRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionPlanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriptionPlan?> GetByIdAsync(Guid id)
    {
        return await _context.SubscriptionPlans
            .FirstOrDefaultAsync(sp => sp.Id == id);
    }

    public async Task<IEnumerable<SubscriptionPlan>> GetAllAsync()
    {
        return await _context.SubscriptionPlans
            .OrderBy(sp => sp.Price)
            .ToListAsync();
    }

    public async Task<IEnumerable<SubscriptionPlan>> GetAllActiveAsync()
    {
        return await _context.SubscriptionPlans
            .Where(sp => sp.IsActive)
            .OrderBy(sp => (double)sp.Price)
            .ToListAsync();
    }

    public async Task AddAsync(SubscriptionPlan plan)
    {
        await _context.SubscriptionPlans.AddAsync(plan);
        await _context.SaveChangesAsync();
    }

    public void Update(SubscriptionPlan plan)
    {
        _context.SubscriptionPlans.Update(plan);
        _context.SaveChanges();
    }

    public async Task<bool> ExistsAsync(Guid planId)
    {
        return await _context.SubscriptionPlans
            .AnyAsync(sp => sp.Id == planId);
    }

    public async Task DeactivatePlanAsync(Guid planId)
    {
        var plan = await GetByIdAsync(planId);
        if (plan != null)
        {
            plan.Deactivate();
            Update(plan);
        }
    }
}
