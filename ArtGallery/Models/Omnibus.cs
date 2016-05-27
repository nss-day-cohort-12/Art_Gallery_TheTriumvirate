using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class Omnibus
    {
        [Key]
        public string Title { get; set; }
        public short ArtistId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Medium { get; set; }
        public string Dimensions { get; set; }
        public short YearMade { get; set; }
        public short NumberMade { get; set; }
        public string EditionsAcquired { get; set; }
        public short NumberInInventory { get; set; }
        public short NumberSold { get; set; }
        public string Locations { get; set; } 
        public string Cost { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
    }
}