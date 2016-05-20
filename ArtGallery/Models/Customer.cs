
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   // using System.Data.Entity.Spatial;

namespace ArtGallery.Models
{

    public class Customer
    {
        public short CustomerId { get; set; }

        public short? AgentId { get; set; }

        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(35)]
        public string LastName { get; set; }

        [StringLength(40)]
        public string Location { get; set; }

        [StringLength(80)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }
    }
}
