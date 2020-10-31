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

        public JobOffersContext(DbContextOptions<JobOffersContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany<JobOffer>(u => u.JobOffers)
                .WithOne()
                .HasForeignKey(jo => jo.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
