﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Retailing.AggregateRoots
{
    public class Product : AggregateRoot
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Model { get; set; } = "";

        [StringLength(50)]
        public string Specifications { get; set; } = "";

        public bool Enabled { get; set; } = true;

        public List<ProductPrice> Prices { get; set; } = new List<ProductPrice>();

        public List<ProductPicture> Pictures { get; set; } = new List<ProductPicture>();
    }
}