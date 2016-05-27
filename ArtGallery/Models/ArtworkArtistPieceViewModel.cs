using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class ArtworkArtistPieceViewModel
    {
        public string Title { get; set; }

        public short NumberInInventory { get; set; }

        public string Name { get; set; }

        public string ImageURL { get; set; }

        public string Medium { get; set; }

        public short ArtworkId { get; set; }

        public string Category { get; set; }

        public short YearOriginalCreated { get; set; }

        public string Dimensions { get; set; }

        public string Location { get; set; }

        public string Cost { get; set; }

        public string Price { get; set; }

        public short NumberMade { get; set; }

        public short NumberSold { get; set; }

        public short PieceId { get; set; }
    }
}