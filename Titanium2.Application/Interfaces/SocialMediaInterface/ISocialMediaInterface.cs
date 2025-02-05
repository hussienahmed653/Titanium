using Titanium2.Application.DTOs;

namespace Titanium2.Application.Interfaces.SocialMediaInterface
{
    public interface ISocialMediaInterface
    {
        public Task<bool> AddSocialMediaAcoount(SocialMediaDTO socialMediaDTO);
        public Task<bool> UpdateSocialMediaAcoount(SocialMediaDTO socialMediaDTO);
        public Task<bool> RemoveSocialMediaAcoount(Guid guid);
    }
}
