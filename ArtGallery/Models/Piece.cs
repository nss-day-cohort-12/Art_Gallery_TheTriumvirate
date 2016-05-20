
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   // using System.Data.Entity.Spatial;

namespace ArtGallery.Models
{

    public class Piece
    {
        public short PieceId { get; set; }

        public short ArtworkId { get; set; }

        [StringLength(128)]
        public string ImageURL { get; set; }

        public DateTime? DateCreated { get; set; }

        public decimal? Cost { get; set; }

        public decimal? Price { get; set; }

        public bool Sold { get; set; }

        [StringLength(40)]
        public string Location { get; set; }

        public short? EditionNumber { get; set; }
    }
}
