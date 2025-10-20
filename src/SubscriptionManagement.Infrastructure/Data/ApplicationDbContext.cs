using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Core.Entities;

namespace SubscriptionManagement.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(sp => sp.Id);
            entity.Property(sp => sp.Name).IsRequired().HasMaxLength(100);
            entity.Property(sp => sp.Description).HasMaxLength(500);
            entity.Property(sp => sp.Price).HasPrecision(18, 2);
            entity.Property(sp => sp.Features).HasConversion(
                v => string.Join(';', v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(us => us.Id);
            entity.HasOne(us => us.SubscriptionPlan)
                .WithMany()
                .HasForeignKey(us => us.SubscriptionPlanId);

            entity.HasIndex(us => new { us.UserId, us.Status });
        });
    }
}
