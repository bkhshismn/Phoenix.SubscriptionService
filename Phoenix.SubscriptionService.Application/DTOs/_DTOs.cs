namespace Phoenix.SubscriptionService.Application.DTOs
{
    public class RegisterRequest
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class AuthResponse
    {
        public required string Token { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
    }

    public class PlanDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public required string Type { get; set; }
    }

    public class SubscriptionDto
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public required string PlanName { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class UserProfileDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public List<SubscriptionDto> Subscriptions { get; set; } = new();
    }
}
