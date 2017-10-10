namespace IDI.Central.Common
{
    public class Configuration
    {
        public class Roles
        {
            public const string Administrators = "Administrators";
            public const string Staffs = "Staffs";
            public const string Customers = "Customers";
        }

        public class ImageSpec
        {
            public static readonly string[] ContentTypes = { "image/jpeg", "image/png" };
            public static readonly string[] Extensions = { ".png", ".jpg", ".jpge" };
            public const long Maximum = 800;
        }

        public class Modules
        {
            public const string Administration = "Administration";
            public const string BasicInfo = "BasicInfo";
            public const string Inventory = "Inventory";
            public const string Logistics = "Logistics";
            public const string Purchase = "Purchase";
            public const string Sales = "Sales";
            public const string Personal = "Personal";
            public const string OAuth = "OAuth";
            public static readonly string[] All = new string[] { Administration, BasicInfo, Inventory, Logistics, Purchase, Sales, Personal, OAuth };
        }

        public class Clients
        {
            public const string Central = "com.idi.central.web";
        }

        public class Inventory
        {
            public const string DefaultBinCode = "P";
        }

        public class OAuthApplication
        {
            public class GitHub
            {
                public const string ClientId = "1cb801da9da98bc98db4";
                public const string ClientSecret = "145147d750c3a3f071bacfe6f7000660fc73bbad";
            }
        }
    }
}
