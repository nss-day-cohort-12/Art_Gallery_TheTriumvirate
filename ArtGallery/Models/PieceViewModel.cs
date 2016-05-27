using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtGallery.Models
{
    public class PieceViewModel
    {
        public short PieceId { get; set; }

        public short ArtworkId { get; set; }

        public string ImageURL { get; set; }

        public string Title { get; set; }

        public string ArtistName { get; set; }

        public short DateCreated { get; set; }

        public string Cost { get; set; }

        public string Price { get; set; }

        public string SoldFor { get; set; }

        public decimal Profit { get; set; }

        public string Location { get; set; }

        public short? EditionNumber { get; set; }

    }
}