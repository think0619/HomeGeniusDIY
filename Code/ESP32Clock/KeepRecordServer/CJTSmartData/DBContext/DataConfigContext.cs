using System;
using System.Collections.Generic; 
using System.Text; 
using Microsoft.EntityFrameworkCore; 
using Entities; 

namespace TextVoiceServer.DBContext
{
    public class DataConfigContext : DbContext
    {
        public DataConfigContext(DbContextOptions<DataConfigContext> options) : base(options)
        {  
        }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          



        }
    }
}
