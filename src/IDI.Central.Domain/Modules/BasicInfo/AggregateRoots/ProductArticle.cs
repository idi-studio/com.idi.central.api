using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IDI.Core.Domain;

namespace IDI.Central.Domain.Modules.BasicInfo.AggregateRoots
{
    [Table("ProductArticle")]
    public class ProductArticle : AggregateRoot
    {
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string ThumbMediaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Authod { get; set; }

        [Required]
        [StringLength(100)]
        public string Digest { get; set; }

        public bool ShowCoverPic { get; set; } = true;

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(256)]
        public string ContentSourceUrl { get; set; }

        public bool NeedOpenComment { get; set; } = false;

        public bool OnlyFansCanComment { get; set; } = false;

        public bool Publish { get; set; } = false;
    }
}
