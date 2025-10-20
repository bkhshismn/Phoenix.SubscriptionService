using Phoenix.SubscriptionService.Application.Interfaces;
using Phoenix.SubscriptionService.Domain.Entities;
using Phoenix.SubscriptionService.Domain.Interfaces;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Plan> _planRepository;

    public SubscriptionService(
        ISubscriptionRepository subscriptionRepository,
        IRepository<User> userRepository,
        IRepository<Plan> planRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _userRepository = userRepository;
        _planRepository = planRepository;
    }

    public async Task<Subscription> SubscribeAsync(int userId, int planId)
    {
        
        var user = await _userRepository.GetByIdAsync(userId);
        var plan = await _planRepository.GetByIdAsync(planId);

        if (user == null || plan == null)
            return null; 

        var subscription = new Subscription
        {
            UserId = userId,
            PlanId = planId,
            StartDate = System.DateTime.UtcNow,
            IsActive = true,
            User = user,   
            Plan = plan    
        };

        await _subscriptionRepository.AddAsync(subscription);
        return subscription;
    }

    public async Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(int userId)
    {
        return await _subscriptionRepository.GetUserSubscriptionsAsync(userId);
    }
}
