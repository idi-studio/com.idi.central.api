using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    [Table("Users")]
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

        public bool IsLocked { get; set; } = false;

        public DateTime? LockTime { get; set; }

        public bool IsActive { get; set; } = true;

        public UserProfile Profile { get; set; } = new UserProfile();

        public UserRole Role { get; set; } = new UserRole();
    }
}
