namespace Titanium2.Domain.UsersRoles
{
    public class RolesModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public ICollection<UsersRolesModel> usersroles { get; set; }
    }
}
