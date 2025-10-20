using Phoenix.SubscriptionService.Application.DTOs;
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

        public async Task<SubscriptionDto?> SubscribeDtoAsync(int userId, int planId)
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
                User = user,   // required
                Plan = plan    // required
            };

            await _subscriptionRepository.AddAsync(subscription);

            return new SubscriptionDto
            {
                Id = subscription.Id,
                PlanId = plan.Id,
                PlanName = plan.Name,
                IsActive = subscription.IsActive,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate
            };
        }

        public async Task<List<SubscriptionDto>> GetUserSubscriptionsDtoAsync(int userId)
        {
            var subscriptions = await _subscriptionRepository.GetUserSubscriptionsAsync(userId);

            return subscriptions.Select(s => new SubscriptionDto
            {
                Id = s.Id,
                PlanId = s.PlanId,
                PlanName = s.Plan.Name,
                IsActive = s.IsActive,
                StartDate = s.StartDate,
                EndDate = s.EndDate
            }).ToList();
        }
    }


