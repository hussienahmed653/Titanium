namespace Titanium2.Domain.UsersRoles
{
    public class UsersRolesModel
    {
        public int UserId { get; set; }
        public UsersModel User { get; set; }
        public int RoleId { get; set; }
        public RolesModel Role { get; set; }
    }
}
