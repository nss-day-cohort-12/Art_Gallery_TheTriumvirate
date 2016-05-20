
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

        public short? ArtworkId { get; set; }

        [StringLength(40)]
        public string ShowLocation { get; set; }

        public bool Agents { get; set; }

        public bool Overhead { get; set; }
    }
}
