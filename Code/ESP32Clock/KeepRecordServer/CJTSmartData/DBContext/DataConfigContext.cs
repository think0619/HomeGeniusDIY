using System;
using System.Collections.Generic; 
using System.Text; 
using Microsoft.EntityFrameworkCore; 
using Entities;
using Entities.Rasp;

namespace TextVoiceServer.DBContext
{
    public class DataConfigContext : DbContext
    {
        public DataConfigContext(DbContextOptions<DataConfigContext> options) : base(options)
        {  
        }
        public DbSet<VLCSource> tb_vlcsrc { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VLCSource>(entity => { entity.HasKey(e => e.RecId); });
        }
    }
}
