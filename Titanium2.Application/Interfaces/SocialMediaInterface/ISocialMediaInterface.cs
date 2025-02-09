using Titanium2.Application.DTOs;
using Titanium2.Domain.SocialMedia;

namespace Titanium2.Application.Interfaces.SocialMediaInterface
{
    public interface ISocialMediaInterface
    {
        public Task<bool> AddSocialMediaAcoount(SocialMediaModel socialMedia);
        public Task<bool> UpdateSocialMediaAcoount(SocialMediaModel socialMedia);
        public Task<bool> RemoveSocialMediaAcoount(SocialMediaModel socialMedia);

        public Task<int> LastId();
        public Task<SocialMediaModel> GetSocialMediaByGuid(Guid guid);
        public Task<SocialMediaModel> GetSocialMediaByUserId(int userid);
    }
}
