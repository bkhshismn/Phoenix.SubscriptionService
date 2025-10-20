using Phoenix.SubscriptionService.Application.Interfaces;
using Phoenix.SubscriptionService.Domain.Entities;
using Phoenix.SubscriptionService.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.SubscriptionService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> RegisterAsync(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };
            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;
            return "JWT_TOKEN_HERE";
        }

        public async Task<User> GetProfileAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var users = await _userRepository.GetAllAsync();
            return users.FirstOrDefault(u => u.Email == email);
        }
    }
}
