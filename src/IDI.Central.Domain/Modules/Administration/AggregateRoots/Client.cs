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
        [StringLength(128)]
        public string SecretKey { get; set; }

        [Required]
        [StringLength(256)]
        public string Salt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
