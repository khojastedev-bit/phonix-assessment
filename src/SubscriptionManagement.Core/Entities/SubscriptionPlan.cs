namespace SubscriptionManagement.Core.Entities;
public class SubscriptionPlan
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int DurationInDays { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public int MaxUsers { get; private set; }
    public List<string> Features { get; private set; }

    private SubscriptionPlan() { } 

    public SubscriptionPlan(string name, string description, decimal price,
        int durationInDays, int maxUsers, List<string> features)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        DurationInDays = durationInDays;
        MaxUsers = maxUsers;
        Features = features;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
    public void UpdatePrice(decimal newPrice) => Price = newPrice;
}
