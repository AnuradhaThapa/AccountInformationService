using AccountInformationService.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountInformationService.Infrastructure.Data
{
    public class AMDBContext : DbContext
    {
        public AMDBContext(DbContextOptions<AMDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientDetail>(e => e.HasKey(x=>x.Id));
        }
        public DbSet<ClientAccountDetail> ClientAccountDetails { get; set; }
        public DbSet<ClientDetail> ClientDetails { get; set; }

    }
}
