using System.Security.Principal;

namespace Titanium2.Application
{
    public class UserRegisterDTO
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; } = string.Empty;
        public char Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
