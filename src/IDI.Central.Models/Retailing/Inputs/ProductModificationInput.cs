using System.Collections.Generic;
using IDI.Core.Common;

namespace IDI.Central.Models.Retailing
{
    public class ProductModificationInput : IInput
    {
        public string Name { get; set; } = "";

        public string Code { get; set; } = "";

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public bool Enabled { get; set; }
    }
}
