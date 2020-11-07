using JobOffersMVC.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC
{
    public class JobOffersContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }

        public DbSet<UserApplication> UserApplications { get; set; }

        public JobOffersContext(DbContextOptions<JobOffersContext> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobOffer>()
                .HasOne(jo => jo.Creator);

            modelBuilder.Entity<JobOffer>()
                .HasMany(jo => jo.UserApplications)
                .WithOne()
                .HasForeignKey(ua => ua.JobOfferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserApplications)
                .WithOne()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplication>()
                .HasOne(ua => ua.User)
                .WithMany(user => user.UserApplications);

            modelBuilder.Entity<UserApplication>()
                .HasOne(ua => ua.JobOffer)
                .WithMany(jo => jo.UserApplications);
        }
    }
}
