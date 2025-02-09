using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.SocialMediaInterface;
using Titanium2.Domain.SocialMedia;
using Titanium2.Domain.UserRepo;

namespace Titanium2.Application.Services
{
    public class SocialMediaServices
    {
        ISocialMediaInterface _socialMediaInterface;
        IUserRepo _userRepo;

        public SocialMediaServices(ISocialMediaInterface socialMediaInterface,
            IUserRepo userRepo)
        {
            _socialMediaInterface = socialMediaInterface;
            _userRepo = userRepo;
        }
        public async Task<bool> AddSocialMediaAcoount(SocialMediaDTO socialMediaDTO)
        {
            var lastid = await _socialMediaInterface.LastId();
            var useridiscorrect = await _userRepo.UserExist(socialMediaDTO.UsersId);
            if (!useridiscorrect)
                throw new FileNotFoundException($"No user found with this id: {socialMediaDTO.UsersId}");
            var SocialMediaAcoount = new SocialMediaModel
            {
                SocialMediaId = lastid + 1,
                UsersId = socialMediaDTO.UsersId,
                Facebook = socialMediaDTO.Facebook,
                Instagram = socialMediaDTO.Instagram,
                Whatsapp = socialMediaDTO.Whatsapp,
            };
            return await _socialMediaInterface.AddSocialMediaAcoount(SocialMediaAcoount);
        }
        public async Task<bool> UpdateSocialMediaAcoount(SocialMediaDTO socialMediaDTO)
        {
            var data = await _socialMediaInterface.GetSocialMediaByUserId(socialMediaDTO.UsersId);
            if (data is null)
                throw new FileNotFoundException("There is no SocialMediaAccounts found with this User");
            
            data.Facebook = !string.IsNullOrEmpty(socialMediaDTO.Facebook) ? socialMediaDTO.Facebook : data.Facebook;
            data.Instagram = !string.IsNullOrEmpty(socialMediaDTO.Instagram) ? socialMediaDTO.Instagram : data.Instagram;
            data.Whatsapp = !string.IsNullOrEmpty(socialMediaDTO.Whatsapp) ? socialMediaDTO.Whatsapp : data.Whatsapp;

            return await _socialMediaInterface.UpdateSocialMediaAcoount(data);
        }
        public async Task<bool> RemoveSocialMediaAcoount(Guid guid)
        {
            var data = await _socialMediaInterface.GetSocialMediaByGuid(guid);
            if (data is null)
                throw new FileNotFoundException("No socialmedia found");
            return await _socialMediaInterface.RemoveSocialMediaAcoount(data);
        }
    }
}
