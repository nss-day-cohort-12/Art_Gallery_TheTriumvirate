using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Omnibus
    {
        public string Title { get; set; }
        public Int16 ArtistId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Medium { get; set; }
        public string Dimensions { get; set; }
        public Int16 YearMade { get; set; }
        public Int16 NumberMade { get; set; }
        public string EditionsAcquired { get; set; }
        public Int16 NumberInInventory { get; set; }
        public Int16 NumberSold { get; set; }
        public string Cost { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
    }
}