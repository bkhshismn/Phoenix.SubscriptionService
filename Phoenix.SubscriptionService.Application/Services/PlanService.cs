using Phoenix.SubscriptionService.Application.Interfaces;
using Phoenix.SubscriptionService.Domain.Entities;
using Phoenix.SubscriptionService.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.SubscriptionService.Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IRepository<Plan> _planRepository;

        public PlanService(IRepository<Plan> planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<IEnumerable<Plan>> GetAllPlansAsync()
        {
            return await _planRepository.GetAllAsync();
        }
    }
}
