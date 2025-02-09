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

        public async Task<bool> AddSocialMediaAcoount(SocialMediaModel socialMedia)
        {
            try
            {
                await _context.SocialMedias.AddAsync(socialMedia);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }
        public async Task<bool> RemoveSocialMediaAcoount(SocialMediaModel socialMedia)
        {
            _context.SocialMedias.Remove(socialMedia);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSocialMediaAcoount(SocialMediaModel socialMedia)
        {
            _context.SocialMedias.Update(socialMedia);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<int> LastId()
        {
            return await _context.SocialMedias.AnyAsync() ? await _context.SocialMedias.MaxAsync(sm => sm.SocialMediaId) : 0;
        }

        public async Task<SocialMediaModel> GetSocialMediaByGuid(Guid guid)
        {
            return await _context.SocialMedias
                .SingleOrDefaultAsync(sm => sm.SocialMediaGuid == guid);
        }

        public async Task<SocialMediaModel> GetSocialMediaByUserId(int userid)
        {
            return await _context.SocialMedias
                .SingleOrDefaultAsync(sm => sm.UsersId == userid);
        }
    }
}
