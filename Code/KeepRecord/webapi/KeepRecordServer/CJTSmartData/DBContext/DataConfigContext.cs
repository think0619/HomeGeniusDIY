﻿using System;
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
        public DbSet<KeepRecord> tb_keeprecord { get; set; }
        public DbSet<KeepType> tb_keeptype { get; set; }
        public DbSet<KeepUnits> tb_keepunits { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KeepRecordView>().ToView(nameof(view_keeprecord)).HasKey(t => t.RecID); 
            modelBuilder.Entity<KeepRecord>(entity =>{ entity.HasKey(e => e.RecID); });
            modelBuilder.Entity<KeepType>(entity =>{ entity.HasKey(e => e.Id); });
            modelBuilder.Entity<KeepUnits>(entity =>{ entity.HasKey(e => e.Id); });



        }
    }
}
