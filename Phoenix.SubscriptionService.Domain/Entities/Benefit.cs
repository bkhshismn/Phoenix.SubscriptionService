namespace Phoenix.SubscriptionService.Domain.Entities
{
    public class Benefit : BaseEntity
    {
        public int PlanId { get; set; }
        public required string Title { get; set; }
        public required string Value { get; set; }
        public required Plan Plan { get; set; }
    }
}
