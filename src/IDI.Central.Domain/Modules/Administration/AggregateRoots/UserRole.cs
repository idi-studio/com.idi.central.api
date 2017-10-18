using System;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    [Table("UserRole")]
    public class UserRole : AggregateRoot
    {
        public Guid UserId { get; set; }

        /// <summary>
        /// Json Type: list of string
        /// </summary>
        public string Roles { get; set; }
    }
}
