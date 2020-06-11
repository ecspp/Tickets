using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tickets.WebAPI.Services;

namespace Tickets.WebAPI.Controllers.v1
{
    [Authorize]
    [ApiController]
    public class FollowupController
    {
        private readonly IFollowupService _followupService;
        private readonly IMapper _mapper;
        public FollowupController(IFollowupService followupService, IMapper mapper)
        {
            this._mapper = mapper;
            this._followupService = followupService;
        }

        // public async Task<ActionResult> Create([FromBody] FollowupCreateRequest createRequest)
        // {
            
        // }
    }
}