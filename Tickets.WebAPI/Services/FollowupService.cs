using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;
using Tickets.WebAPI.Data;
using Tickets.WebAPI.Extensions;

namespace Tickets.WebAPI.Services
{
    public class FollowupService : IFollowupService
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAcessor;
        public FollowupService(UserManager<User> userManager, DataContext dataContext, IHttpContextAccessor httpContextAcessor)
        {
            this._httpContextAcessor = httpContextAcessor;
            this._dataContext = dataContext;
            this._userManager = userManager;
        }
        public async Task<bool> CreateFollowupAsync(Followup followup)
        {
            followup.CreatedAt = DateTime.UtcNow;
            await _dataContext.Followups.AddAsync(followup);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteFollowupAsync(Guid followupId)
        {
            var followup = await GetFollowupByIdAsync(followupId);
            _dataContext.Remove(followup);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<ICollection<Followup>> GetAllFollowupsAsync()
        {
            var companyId = _httpContextAcessor.GetCompanyId();
            return await _dataContext.Followups.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Followup> GetFollowupByIdAsync(Guid followupId)
        {
            var companyId = _httpContextAcessor.GetCompanyId();
            return await _dataContext.Followups.FirstOrDefaultAsync(x => x.Id == followupId && x.CompanyId == companyId);
        }

        public async Task<bool> UpdateFollowupAsync(Followup followup)
        {
            followup.UpdatedAt = DateTime.UtcNow;
            _dataContext.Followups.Update(followup);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}