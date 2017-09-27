namespace IDI.Core.Common
{
    public class Constants
    {
        public class Headers
        {
            public const string Authorization = "Authorization";
        }

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
            public const string AllowCORSDomain = "AllowCORSDomain";
        }

        public class SessionKey
        {
            public const string CurrentUser = "CURRENT-USER";
        }

        public class LoggerCategory
        {
            public const string Info = "Info";
            public const string Debug = "Debug";
            public const string Error = "Error";
        }
    }
}
