
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.Spatial;

namespace ArtGallery.Models
{

    public class Piece2
    {
        public Int16 PieceId { get; set; }

        public Int16 ArtworkId { get; set; }

        [StringLength(128)]
        public string ImageURL { get; set; }

        public string DateCreated { get; set; }

        public string Cost { get; set; }

        public string Price { get; set; }

        public string SoldFor { get; set; }

        [StringLength(40)]
        public string Location { get; set; }

        public short? EditionNumber { get; set; }
    }
}
