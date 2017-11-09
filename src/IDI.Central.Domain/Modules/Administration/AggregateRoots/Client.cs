using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.Administration.AggregateRoots
{
    [Table("Clients")]
    public class Client : AggregateRoot
    {
        [Required]
        [StringLength(50)]
        public string ClientId { get; set; }

        [Required]
        [StringLength(1024)]
        public string SecretKey { get; set; }

        public bool Active { get; set; } = true;
    }
}
