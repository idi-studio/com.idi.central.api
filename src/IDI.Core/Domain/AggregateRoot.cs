using System;
using System.ComponentModel.DataAnnotations;
using IDI.Core.Common;

namespace IDI.Core.Domain
{
    public abstract class AggregateRoot: IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [ConcurrencyCheck]
        public int Version { get; set; }

        [Required]
        [StringLength(256)]
        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        [Required]
        [StringLength(256)]
        public string LastUpdatedBy { get; set; }

        public DateTime LastUpdatedTime { get; set; }

        public AggregateRoot()
        {
            this.Id = Guid.NewGuid();
            this.Version = 1;
            this.CreatedBy = "-";
            this.CreatedTime = DateTime.Now;
            this.LastUpdatedBy = "-";
            this.LastUpdatedTime = DateTime.Now;
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(this.Id.GetHashCode());
        }
    }
}
