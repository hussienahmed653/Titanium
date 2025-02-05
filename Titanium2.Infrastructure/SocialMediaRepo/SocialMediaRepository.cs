using Microsoft.EntityFrameworkCore;
using System;
using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.SocialMediaInterface;
using Titanium2.Domain.SocialMedia;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.SocialMediaRepo
{
    public class SocialMediaRepository : ISocialMediaInterface
    {
        ApplicationDbContext _context;

        public SocialMediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSocialMediaAcoount(SocialMediaDTO socialMediaDTO)
        {
            try
            {
                var lastid = await _context.SocialMedias.AnyAsync() ? await _context.SocialMedias.MaxAsync(sm => sm.SocialMediaId) : 0;
                var useridiscorrect = await _context.users.AnyAsync(u => u.UserId == socialMediaDTO.UsersId);
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
                await _context.SocialMedias.AddAsync(SocialMediaAcoount);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error, {ex.Message}");
            }

        }

        public async Task<bool> RemoveSocialMediaAcoount(Guid guid)
        {
            var data = await _context.SocialMedias.SingleOrDefaultAsync(sm => sm.SocialMediaGuid == guid);
            if (data is null)
                throw new FileNotFoundException("No socialmedia found");
            _context.SocialMedias.Remove(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSocialMediaAcoount(SocialMediaDTO socialMediaDTO)
        {
            var data = await _context.SocialMedias
                .SingleOrDefaultAsync(sm => sm.SocialMediaGuid == socialMediaDTO.SocialMediaGuid);

            data.Facebook = !string.IsNullOrEmpty(socialMediaDTO.Facebook) ? socialMediaDTO.Facebook : data.Facebook;
            data.Instagram = !string.IsNullOrEmpty(socialMediaDTO.Instagram) ? socialMediaDTO.Instagram : data.Instagram;
            data.Whatsapp = !string.IsNullOrEmpty(socialMediaDTO.Whatsapp) ? socialMediaDTO.Whatsapp : data.Whatsapp;
            _context.SocialMedias.Update(data);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
