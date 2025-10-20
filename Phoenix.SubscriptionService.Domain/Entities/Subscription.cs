using System; 
namespace Phoenix.SubscriptionService.Domain.Entities 
{
    public class Subscription : BaseEntity
    {
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public required User User { get; set; }
        public required Plan Plan { get; set; }
    }

}
