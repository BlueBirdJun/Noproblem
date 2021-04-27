using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Repository.NopModels;

namespace Nop.Repository
{
    public class NopContext :DbContext
    {
        public DbSet<CommonBoards> boards { get; set; }
        public DbSet<CommonCodes> Commoncodes { get; set; }
        public DbSet<Member> Member { get; set; }

        public NopContext()
        {

        }
        

        public NopContext(DbContextOptions<NopContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql("Server=localhost;User ID=root;Password=abc123;Database=NOP;",
            //    options => options
            //        .CharSet(CharSet.Utf8)
            //        .CharSetBehavior(CharSetBehavior.AppendToAllColumns));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .Property(b => b.CreateDate)                
                .HasDefaultValueSql("current_timestamp()");

            modelBuilder.Entity<Member>()
                .HasIndex(p => new { p.UserId, p.idx })
                .IsUnique();
                

            modelBuilder.Entity<CommonBoards>()
                .Property(b => b.CreateDate)
                .HasDefaultValueSql("current_timestamp()");

            modelBuilder.Entity<CommonCodes>()
                .Property(b => b.CreateDate)
                .HasDefaultValueSql("current_timestamp()");
        }

    }
}
