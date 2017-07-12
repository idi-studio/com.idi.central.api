using System.ComponentModel.DataAnnotations;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    public class Client : AggregateRoot
    {
        [Required]
        [StringLength(20)]
        public string ClientId { get; set; }

        [Required]
        [StringLength(128)]
        public string SecretKey { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
