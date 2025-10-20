using Microsoft.AspNetCore.Mvc;
using Phoenix.SubscriptionService.Application.DTOs;
using Phoenix.SubscriptionService.Application.Interfaces;
using System.Threading.Tasks;

namespace Phoenix.SubscriptionService.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await _userService.RegisterAsync(request.Username, request.Email, request.Password);
            return Ok(new { user.Id, user.Username, user.Email });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _userService.LoginAsync(request.Email, request.Password);
            if (token == null) return Unauthorized();

            var user = await _userService.GetByEmailAsync(request.Email);
            return Ok(new AuthResponse
            {
                Token = token,
                Email = user.Email,
                Username = user.Username
            });
        }
    }

    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile([FromQuery] int userId)
        {
            var user = await _userService.GetProfileAsync(userId);
            return Ok(user);
        }
    }

    [ApiController]
    [Route("api/plans")]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _planService.GetAllPlansAsync();
            return Ok(plans);
        }
    }

    [ApiController]
    [Route("api/subscriptions")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromQuery] int userId, [FromQuery] int planId)
        {
            var subscription = await _subscriptionService.SubscribeAsync(userId, planId);
            return Ok(subscription);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSubscriptions([FromQuery] int userId)
        {
            var subscriptions = await _subscriptionService.GetUserSubscriptionsAsync(userId);
            return Ok(subscriptions);
        }
    }
}
