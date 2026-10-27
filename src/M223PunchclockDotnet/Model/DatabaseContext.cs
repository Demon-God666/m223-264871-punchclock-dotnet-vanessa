using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace M223PunchclockDotnet.Model
{

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }


        public DbSet<Entry> Entries { get; set; }
        public DbSet<Category> Categories {get;set;}
        public DbSet<Tag> Tags {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");

            modelBuilder.Entity<Entry>().ToTable("Entry")
            .HasOne(entry => entry.Category)
            .WithMany(category => category.Entries)
            .HasForeignKey(entry => entry.Category_ID)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tag>().ToTable("Tag");

            modelBuilder.Entity<Entry>().ToTable("Entry")
            .HasMany(entry => entry.Tags)
            .WithMany(tags => tags.Entries)
            .UsingEntity("entry_tag", tag => tag.HasOne(typeof(Tag)).WithMany().HasForeignKey("tag_id"),
            entry => entry.HasOne(typeof(Entry)).WithMany().HasForeignKey("entry_id")
            );

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseNpgsql("Host=db;Database=postgres;Username=postgres;Password=postgres");



    }
}