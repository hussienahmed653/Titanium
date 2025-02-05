using Titanium2.Domain.UsersRoles;

namespace Titanium2.Application.DTOs
{
    public class SocialMediaDTO
    {
        public int? SocialMediaId { get; set; }
        public Guid SocialMediaGuid { get; set; } = Guid.NewGuid();
        public int UsersId { get; set; }
        public string Facebook { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Whatsapp { get; set; } = string.Empty;
    }
}
