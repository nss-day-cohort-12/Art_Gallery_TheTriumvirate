
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   // using System.Data.Entity.Spatial;

namespace ArtGallery.Models
{

    public class Artwork
    {
        public short ArtworkId { get; set; }

        public short ArtistId { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        public short? YearOriginalCreated { get; set; }

        [StringLength(60)]
        public string Medium { get; set; }

        [StringLength(60)]
        public string Dimensions { get; set; }

        public short? NumberMade { get; set; }

        public short NumberInInventory { get; set; }

        public short NumberSold { get; set; }
    }
}
