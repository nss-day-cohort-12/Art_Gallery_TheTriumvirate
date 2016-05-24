
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

namespace ArtGallery.Models
{

    public class Artwork
    {
        public Int16 ArtworkId { get; set; }

        public Int16 ArtistId { get; set; }

        public string Title { get; set; }

        public Int16 YearOriginalCreated { get; set; }

        public string Medium { get; set; }

        public string Category { get; set; }

        public string Dimensions { get; set; }

        public Int16 NumberMade { get; set; }

        public Int16 NumberInInventory { get; set; }

        public Int16 NumberSold { get; set; }
    }
}
