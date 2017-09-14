namespace IDI.Central.Common
{
    public class Constants
    {
        public class Roles
        {
            public const string Administrators = "Administrators";
            public const string Staffs = "Staffs";
            public const string Customers = "Customers";
        }

        public class Config
        {
            public class ImageSpec
            {
                public static readonly string[] ContentTypes = { "image/jpeg", "image/png" };
                public static readonly string[] Extensions = { ".png", ".jpg", ".jpge" };
                public const long Maximum = 800;
            }
           
        }
    }
}
