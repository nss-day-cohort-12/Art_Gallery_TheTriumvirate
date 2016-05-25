﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ArtGallery.Models
{
    public class ArtGalleryDbContext : DbContext
    {
        public DbSet<Artwork> Artworkz { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        public DbSet<Agent> Agents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artwork>()
                .ToTable("Artwork")
                .HasKey(a => a.ArtworkId);

            modelBuilder.Entity<Artist>()
                .ToTable("Artist")
                .HasKey(ar => ar.ArtistId);

            modelBuilder.Entity<Piece>()
                .ToTable("Piece")
                .HasKey(p => p.PieceId);

            modelBuilder.Entity<Agent>()
                .ToTable("Agent")
                .HasKey(g => g.AgentId);
        }
    }
}