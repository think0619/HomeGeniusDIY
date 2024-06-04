using System;
using System.Collections.Generic; 
using System.Text; 
using Microsoft.EntityFrameworkCore; 
using Entities;
using Entities.Rasp;
using Entities.Net;
using Entities.Content;
using Entities.User;

namespace TextVoiceServer.DBContext
{
    public class DataConfigContext : DbContext
    {
        public DataConfigContext(DbContextOptions<DataConfigContext> options) : base(options)
        {  
        }
        public DbSet<VLCSource> tb_vlcsrc { get; set; }
        public DbSet<NetInfo> tb_config { get; set; }
        public DbSet<APiConfig> tb_apiconfig { get; set; }
        public DbSet<ESPConfig> tb_espconfig { get; set; }
        public DbSet<LoginUser> tb_user { get; set; }
        public DbSet<LocalFileSrc> tb_localfile { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VLCSource>(entity => { entity.HasKey(e => e.RecId); });
            modelBuilder.Entity<NetInfo>(entity => { entity.HasKey(e => e.RecId); });
            modelBuilder.Entity<APiConfig>(entity => { entity.HasKey(e => e.recid); });
            modelBuilder.Entity<ESPConfig>(entity => { entity.HasKey(e => e.recid); });
            modelBuilder.Entity<LoginUser>(entity => { entity.HasKey(e => e.Id); });
            modelBuilder.Entity<LocalFileSrc>(entity => { entity.HasKey(e => e.Id); });
        }
    }
}
