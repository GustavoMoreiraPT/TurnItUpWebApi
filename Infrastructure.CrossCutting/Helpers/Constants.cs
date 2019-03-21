namespace Infrastructure.CrossCutting.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
                public const string Events = "eve", EventsId = "asot";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
                public const string EventsAccess = "events_access";
            }
        }
    }
}
