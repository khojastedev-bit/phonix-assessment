using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Core.Entities;

namespace SubscriptionManagement.Infrastructure.Data;

public static class SeedData
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        // Ensure the database is created and migrations are applied
        await context.Database.MigrateAsync();

        // Check if we already have data
        if (await context.SubscriptionPlans.AnyAsync())
        {
            return; // Database already seeded
        }

        var plans = new List<SubscriptionPlan>
        {
            new SubscriptionPlan(
                name: "Basic Plan",
                description: "Perfect for individuals getting started",
                price: 9.99m,
                durationInDays: 30,
                maxUsers: 1,
                features: new List<string> { "10 Projects", "5GB Storage", "Basic Support" }
            ),
            new SubscriptionPlan(
                name: "Pro Plan",
                description: "Great for small teams and growing businesses",
                price: 19.99m,
                durationInDays: 30,
                maxUsers: 5,
                features: new List<string> { "50 Projects", "50GB Storage", "Priority Support", "API Access" }
            ),
            new SubscriptionPlan(
                name: "Enterprise Plan",
                description: "For large organizations with advanced needs",
                price: 49.99m,
                durationInDays: 30,
                maxUsers: 50,
                features: new List<string> { "Unlimited Projects", "500GB Storage", "24/7 Support", "API Access", "Custom Integrations" }
            )
        };

        await context.SubscriptionPlans.AddRangeAsync(plans);
        await context.SaveChangesAsync();
    }
}