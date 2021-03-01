using MailingGroups.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingGroups.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         //   modelBuilder.Entity<GroupModel>()
           //     .HasAlternateKey(g => g.Name);

         //   modelBuilder.Entity<MailModel>()
           //    .HasKey(m => m.Id);

            //modelBuilder.Entity<MailModel>()
              // .HasKey(m => m.Id);
        }*/

        public DbSet<GroupType> Groups { get; set; }

        public DbSet<MailsType> Mails { get; set; }
    }
}
