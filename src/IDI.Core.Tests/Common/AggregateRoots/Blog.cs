using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Core.Tests.Common.AggregateRoots
{
    public class Blog : AggregateRoot
    {
        [Required]
        [MaxLength(512)]
        public string Url { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
