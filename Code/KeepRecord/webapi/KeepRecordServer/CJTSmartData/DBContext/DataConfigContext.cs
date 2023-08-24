using System;
using System.Collections.Generic; 
using System.Text; 
using Microsoft.EntityFrameworkCore;

using Entities;
using Entities.Keep;

namespace TextVoiceServer.DBContext
{
    public class DataConfigContext : DbContext
    {
        public DataConfigContext(DbContextOptions<DataConfigContext> options) : base(options)
        {  
        } 
         
         public DbSet<KeepRecordView> view_keeprecord { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KeepRecordView>().ToView(nameof(view_keeprecord)).HasKey(t => t.RecID);

            //modelBuilder.Entity<NewGold>(entity =>
            //{
            //    entity.HasKey(e => e.TestNo);
            //});



        }
    }
}
