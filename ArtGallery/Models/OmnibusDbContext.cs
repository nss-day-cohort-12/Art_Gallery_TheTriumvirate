using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ArtGallery.Models
{
    public class OmnibusDbContext : DbContext
    {
        public DbSet<Omnibus> Omni { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Omnibus>()
                .ToTable("Omnibus")
                .HasKey(omni => omni.ArtistId);
        }
    }
}