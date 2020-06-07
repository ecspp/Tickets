using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tickets.Domain;

namespace Tickets.WebAPI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly UserManager<User> _userManager = null;

        protected Guid GetUserId()
        {
            if (User != null)
            {
                var userIdClaim = User.FindFirst("id");
                if (userIdClaim != null)
                {
                    return Guid.Parse(userIdClaim.Value);
                }
            }
            return Guid.Empty;
        }

        protected Guid GetCompanyId()
        {
            if (User != null)
            {
                var companyIdClaim = User.FindFirst("companyId");
                if (companyIdClaim != null)
                {
                    return Guid.Parse(companyIdClaim.Value);
                }
            }
            return Guid.Empty;
        }

    }
}