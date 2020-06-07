namespace Tickets.WebAPI.Contracts.v1
{
    public class ApiRoutes
    {
        public const string Version = "v1";
        public const string Root = "api";
        public const string Base = Root + "/" + Version;

        public static class Auth
        {
            public const string Login = Base + "/auth/login";
            public const string Register = Base + "/auth/register";
            public const string Refresh = Base + "/auth/refresh";
        }

        public static class Ticket
        {
            public const string GetAll = Base + "/ticket";
            public const string Create = Base + "/ticket";
            public const string Update = Base + "/ticket/{ticketId}";
            public const string Delete = Base + "/ticket/{ticketId}";
            public const string Get = Base + "/ticket/{ticketId}";
        }
    }
}