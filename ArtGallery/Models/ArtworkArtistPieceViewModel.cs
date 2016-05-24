using System;
using System.Collections.Generic;
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

        public string Category { get; set; }

        public string Medium { get; set; }

        public short ArtworkId { get; set; }
    }
}