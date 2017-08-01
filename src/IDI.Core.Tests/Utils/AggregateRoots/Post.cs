using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Domain;

namespace IDI.Core.Tests.Utils.AggregateRoots
{
    public class Post : AggregateRoot
    {
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        [Required]
        [MaxLength(512)]
        public string Content { get; set; }

        public Guid BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}
