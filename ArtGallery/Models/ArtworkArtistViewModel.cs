using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class ArtworkArtistViewModel
    {

        public string Title { get; set; }

        public short NumberInInventory { get; set; }

        public string Name { get; set; }

        public short? NumberMade { get; set; }
    }
}