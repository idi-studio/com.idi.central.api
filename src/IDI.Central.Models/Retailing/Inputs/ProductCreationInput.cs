using IDI.Core.Common;

namespace IDI.Central.Models.Retailing
{
    public class ProductCreationInput : IInput
    {
        public string Name { get; set; } = "";

        public string Code { get; set; } = "";

        public string Profile { get; set; } = "";
    }
}
