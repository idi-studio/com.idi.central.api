using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Core.Tests.Common.AggregateRoots
{
    public class User : AggregateRoot
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(256)]
        public string Password { get; set; }
    }
}
