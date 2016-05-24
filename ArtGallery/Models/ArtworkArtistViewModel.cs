using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class ArtworkArtistViewModel
    {

        public string Title { get; set; }

        public Int16 NumberInInventory { get; set; }

        public string Name { get; set; }

        public Int16? NumberMade { get; set; }
    }
}