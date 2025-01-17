using System.Security.Principal;
using System;
using Microsoft.AspNetCore.Http;
using Tickets.WebAPI.Exceptions;

namespace Tickets.WebAPI.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static int GetUserId(this IHttpContextAccessor acessor)
        {
            if (acessor.HttpContext.User != null)
            {
                int id;
                var isInt = int.TryParse(acessor.HttpContext.User.FindFirst("id").Value, out id);
                if (isInt)
                {
                    return id;
                }
            }
            throw new InvalidUserIdException();
        }

        public static int GetCompanyId(this IHttpContextAccessor acessor)
        {
            if (acessor.HttpContext.User != null)
            {
                int id;
                var isInt = int.TryParse(acessor.HttpContext.User.FindFirst("companyId").Value, out id);
                if (isInt)
                {
                    return id;
                }
            }
            throw new InvalidCompanyIdException();
        }
    }
}