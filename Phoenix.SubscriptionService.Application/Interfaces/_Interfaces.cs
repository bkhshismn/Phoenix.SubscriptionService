using Phoenix.SubscriptionService.Application.DTOs;
using Phoenix.SubscriptionService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.SubscriptionService.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string username, string email, string password);
        Task<string> LoginAsync(string email, string password);
        Task<User> GetProfileAsync(int userId);
        Task<User> GetByEmailAsync(string email);
    }

    public interface IPlanService
    {
        Task<IEnumerable<Plan>> GetAllPlansAsync();
    }

    public interface ISubscriptionService
    {
        Task<SubscriptionDto> SubscribeDtoAsync(int userId, int planId);
        Task<List<SubscriptionDto>> GetUserSubscriptionsDtoAsync(int userId);
    }

}
