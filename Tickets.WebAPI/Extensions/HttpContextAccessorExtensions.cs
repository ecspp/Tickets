using System;
using Microsoft.AspNetCore.Http;

namespace Tickets.WebAPI.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Guid GetUserId(this IHttpContextAccessor acessor)
        {
            if (acessor.HttpContext.User != null)
            {
                try
                {
                    var userIdClaim = acessor.HttpContext.User.FindFirst("id");
                    if (userIdClaim != null)
                    {
                        return Guid.Parse(userIdClaim.Value);
                    }
                    
                }
                catch (System.Exception)
                {
                    // TODO: Log erro de Guid, não deverria existir aqui
                }
            }
            return Guid.Empty;
        }

        public static Guid GetCompanyId(this IHttpContextAccessor acessor)
        {
            if (acessor.HttpContext.User != null)
            {
                try
                {
                    var companyIdClaim = acessor.HttpContext.User.FindFirst("companyId");
                    if (companyIdClaim != null)
                    {
                        return Guid.Parse(companyIdClaim.Value);
                    }
                }
                catch (System.Exception)
                {
                    // TODO: Log erro de Guid, não deverria existir aqui
                }
            }
            return Guid.Empty;
        }
    }
}