using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.SCM.AggregateRoots
{
    public class UserProfile : AggregateRoot
    {
        public Guid UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Photo { get; set; } = "default.jpg";

        public Gender Gender { get; set; } = Gender.Unknown;

        public DateTime Birthday { get; set; } = new DateTime(1980, 1, 1);

        public User User { get; set; }
    }
}
