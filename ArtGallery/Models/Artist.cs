
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    //using System.Data.Entity.Spatial;


namespace ArtGallery.Models
{

    public class Artist
    {
        public short ArtistId { get; set; }

        [Required]
        [StringLength(65)]
        public string Name { get; set; }

        public short? BirthYear { get; set; }

        public short? DeathYear { get; set; }
    }
}
