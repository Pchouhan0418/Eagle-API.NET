using Eagle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagle.Infrastructure.Context
{
    public class EagleContext : DbContext
    {
        public EagleContext(DbContextOptions<EagleContext> options) : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("tblEvents");
        }
    }
}
