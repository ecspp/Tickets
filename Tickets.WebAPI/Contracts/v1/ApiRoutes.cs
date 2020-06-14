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

        public static class Contact
        {
            public const string GetAll = Base + "/contact";
            public const string Create = Base + "/contact";
            public const string Update = Base + "/contact/{contactId}";
            public const string Delete = Base + "/contact/{contactId}";
            public const string Get = Base + "/contact/{contactId}";
        }

        public static class Followup
        {
            public const string Create = Base + "/followup";
            public const string Update = Base + "/followup/{followupId}";
            public const string Delete = Base + "/followup/{followupId}";
            public const string Get = Base + "/followup/{followupId}";
            public const string GetAllFromTicket = Base + "/followup/ticket/{ticketId}";
        }

        public static class ContactType
        {
            public const string GetAll = Base + "/contacttype";
            public const string Create = Base + "/contacttype";
            public const string Update = Base + "/contacttype/{contactTypeId}";
            public const string Delete = Base + "/contacttype/{contactTypeId}";
            public const string Get = Base + "/contacttype/{contactTypeId}";
        }
    }
}