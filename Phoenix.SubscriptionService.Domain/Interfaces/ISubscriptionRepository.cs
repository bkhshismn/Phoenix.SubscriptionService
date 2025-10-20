using Phoenix.SubscriptionService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.SubscriptionService.Domain.Interfaces
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(int userId);
    }
}
