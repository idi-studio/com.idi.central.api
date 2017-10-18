using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Common;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    [Table("UserProfile")]
    public class UserProfile : AggregateRoot
    {
        public Guid UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string PhoneNum { get; set; }

        public bool PhoneVerified { get; set; } = false;

        [StringLength(100)]
        public string Email { get; set; }

        public bool EmailVerified { get; set; } = false;

        [Required]
        [StringLength(20)]
        public string Photo { get; set; } = "user.png";

        public Gender Gender { get; set; } = Gender.Unknown;

        public DateTime Birthday { get; set; } = new DateTime(1980, 1, 1);
    }
}
