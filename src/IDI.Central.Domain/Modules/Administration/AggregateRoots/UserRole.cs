using System;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    public class UserRole : AggregateRoot
    {
        public Guid UserId { get; set; }

        /// <summary>
        /// Json Type: list of string
        /// </summary>
        public string Roles { get; set; }
    }
}
