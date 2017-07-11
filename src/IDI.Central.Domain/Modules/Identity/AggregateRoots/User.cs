using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Identity.AggregateRoots
{
    public class User : AggregateRoot
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(256)]
        public string Salt { get; set; }

        [Required]
        [StringLength(256)]
        public string Password { get; set; }

        public bool IsActive { get; set; } = true;

        public UserProfile Profile { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
