namespace Titanium2.Domain.UsersRoles
{
    public class UsersModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        public string Address { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public char Gender { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Password { get; set; } = string.Empty;
        public IEnumerable<UsersRolesModel> usersRoles { get; set; }
    }
}
