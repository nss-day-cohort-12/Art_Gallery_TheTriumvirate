using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class ArtShowVM
    {
        public short ArtShowId { get; set; }

        public string ArtistsRepresented { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string ShowLocation { get; set; }

        public string Agents { get; set; }

        public string Overhead { get; set; }
    }
}