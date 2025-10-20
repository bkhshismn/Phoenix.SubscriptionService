using System.Collections.Generic;
using Phoenix.SubscriptionService.Domain.Enums;

namespace Phoenix.SubscriptionService.Domain.Entities
{
    public class Plan : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public PlanType Type { get; set; }
        public ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
