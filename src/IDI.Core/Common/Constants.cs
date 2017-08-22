namespace IDI.Core.Common
{
    public class Constants
    {
        public class AuthenticationMethod
        {
            public const string ClientCredentials = "client_credentials";
            public const string Password = "password";
        }
        public class AuthenticationScheme
        {
            public const string Basic = "Basic";
            public const string Bearer = "Bearer";
        }

        public class Policy
        {
            public const string AllowCorsDomain = "AllowCorsDomain";
        }

        public class VerificationGroup
        {
            public const string Default = "Default";
            public const string Create = "Create";
            public const string Update = "Update";
            public const string CreateOrUpdate = "Create,Update";
            public const string Delete = "Delete";
        }

        public class SessionKey
        {
            public const string CurrentUser = "CURRENT-USER";
        }
    }
}
