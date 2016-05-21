using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ArtGallery.Models
{
    public class ArtworkDbContext : DbContext
    {
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artwork>()
                .ToTable("Artwork")
                .HasKey(a => a.ArtworkId);

            modelBuilder.Entity<Artist>()
                .ToTable("Artist")
                .HasKey(ar => ar.ArtistId);
        }
    }
}