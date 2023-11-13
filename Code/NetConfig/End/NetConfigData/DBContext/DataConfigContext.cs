using System;
using System.Collections.Generic; 
using System.Text; 
using Microsoft.EntityFrameworkCore; 
using Entities; 
using Entities.Net;

namespace TextVoiceServer.DBContext
{
    public class DataConfigContext : DbContext
    {
        public DataConfigContext(DbContextOptions<DataConfigContext> options) : base(options)
        {  
        } 
        public DbSet<NetInfo> tb_config { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<KeepRecordView>().ToView(nameof(view_keeprecord)).HasKey(t => t.RecID); 
            modelBuilder.Entity<NetInfo>(entity =>{ entity.HasKey(e => e.RecId); });  
        }
    }
}
