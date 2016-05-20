
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    //using System.Data.Entity.Spatial;

namespace ArtGallery.Models
{
    public class Invoice
    {
        public short InvoiceId { get; set; }

        public short CustomerId { get; set; }

        public short? AgentId { get; set; }

        [StringLength(15)]
        public string PaymentMethod { get; set; }

        [StringLength(80)]
        public string ShippingAddress { get; set; }

        public short PieceId { get; set; }
    }
}
