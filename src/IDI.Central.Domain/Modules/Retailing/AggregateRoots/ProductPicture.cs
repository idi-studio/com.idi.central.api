using System;
using IDI.Central.Domain.Common;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class ProductPicture : AggregateRoot
    {
        public string Name { get; set; }

        public PictureCategory Category { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}
