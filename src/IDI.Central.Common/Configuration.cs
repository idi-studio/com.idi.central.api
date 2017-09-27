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
            public const string Material = "Material";
            public const string Purchase = "Purchase";
            public const string Sales = "Sales";
            public static readonly string[] All = new string[] { Administration, BasicInfo, Inventory, Logistics, Material, Purchase, Sales };
        }

        public class Clients
        {
            public const string Central = "com.idi.central.web";
        }
    }
}
