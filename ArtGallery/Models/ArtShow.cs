
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.Spatial;

namespace ArtGallery.Models
{

    public class ArtShow
    { 

        public short ArtShowId { get; set; }

        public string ArtistsRepresented { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        [StringLength(40)]
        public string ShowLocation { get; set; }

        public string Agents { get; set; }

        public string Overhead { get; set; }
    }
}
