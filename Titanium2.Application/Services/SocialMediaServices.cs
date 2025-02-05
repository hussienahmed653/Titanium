using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.SocialMediaInterface;

namespace Titanium2.Application.Services
{
    public class SocialMediaServices
    {
        ISocialMediaInterface _socialMediaInterface;

        public SocialMediaServices(ISocialMediaInterface socialMediaInterface)
        {
            _socialMediaInterface = socialMediaInterface;
        }
        public async Task<bool> AddSocialMediaAcoount(SocialMediaDTO socialMediaDTO)
        {
            return await _socialMediaInterface.AddSocialMediaAcoount(socialMediaDTO);
        }
        public async Task<bool> UpdateSocialMediaAcoount(SocialMediaDTO socialMediaDTO)
        {
            return await _socialMediaInterface.UpdateSocialMediaAcoount(socialMediaDTO);
        }
        public async Task<bool> RemoveSocialMediaAcoount(Guid guid)
        {
            return await _socialMediaInterface.RemoveSocialMediaAcoount(guid);
        }
    }
}
