using Microsoft.AspNetCore.Http;
using Tickets.WebAPI.Exceptions;

namespace Tickets.WebAPI.Extensions
{
    public static class HttpContextExtensions
    {
         public static int GetUserId(this HttpContext context)
        {
            if (context.User != null)
            {
                int id;
                var isInt = int.TryParse(context.User.FindFirst("id").Value, out id);
                if (isInt)
                {
                    return id;
                }
            }
            throw new InvalidUserIdException();
        }

        public static int GetCompanyId(this HttpContext context)
        {
            if (context.User != null)
            {
                int id;
                var isInt = int.TryParse(context.User.FindFirst("companyId").Value, out id);
                if (isInt)
                {
                    return id;
                }
            }
            throw new InvalidCompanyIdException();
        }
    }
}