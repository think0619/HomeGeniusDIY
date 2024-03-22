using System;
using System.Collections.Generic; 
using System.Text; 
using Microsoft.EntityFrameworkCore; 
using Entities; 
using Entities.Net;
using Entities.Keep;
using Entities.User;

namespace TextVoiceServer.DBContext
{
    public class DataConfigContext : DbContext
    {
        public DataConfigContext(DbContextOptions<DataConfigContext> options) : base(options)
        {
        }
        public DbSet<KeepRecordView> view_keeprecord { get; set; }
        public DbSet<KeepRecord> tb_keeprecord { get; set; }
        public DbSet<KeepType> tb_keeptype { get; set; }
        public DbSet<KeepUnits> tb_keepunits { get; set; }
        public DbSet<NetInfo> tb_config { get; set; }
        public DbSet<LoginUser> tb_user { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KeepRecordView>().ToView(nameof(view_keeprecord)).HasKey(t => t.Id);
            modelBuilder.Entity<KeepRecord>(entity => { entity.HasKey(e => e.Id); });
            modelBuilder.Entity<KeepType>(entity => { entity.HasKey(e => e.Id); });
            modelBuilder.Entity<NetInfo>(entity =>{ entity.HasKey(e => e.Id); });
            modelBuilder.Entity<LoginUser>(entity => { entity.HasKey(e => e.Id); });
        }
    }
}
