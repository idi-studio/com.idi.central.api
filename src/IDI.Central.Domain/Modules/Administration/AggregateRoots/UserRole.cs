using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Common;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    [Table("UserRole")]
    public class UserRole : AggregateRoot
    {
        public Guid UserId { get; set; }

        [JsonData(typeof(List<string>))]
        public string Roles { get; set; }
    }
}
