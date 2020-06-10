using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Domain;

namespace Tickets.WebAPI.Services
{
    public interface IFollowupService
    {
        public Task<bool> CreateFollowupAsync(Followup followup);
        public Task<bool> UpdateFollowupAsync(Followup followup);
        public Task<bool> DeleteFollowupAsync(Guid followupId);
        public Task<Followup> GetFollowupByIdAsync(Guid followupId);
        public Task<ICollection<Followup>> GetAllFollowupsAsync();
    }
}