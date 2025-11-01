using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class HPRSDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server = DESKTOP-4JRJBFR\\SQLEXPRESS; Database = HPRSDB;Trusted_Connection=True;TrustServerCertificate=True");
            optionsBuilder.UseNpgsql("Host=dpg-d42qt10dl3ps73cmkph0-a.singapore-postgres.render.com;Port=5432;Database=hprs_db;Username=hprs_db_user;Password=DGKhVt6i0XciDY4AsXFuMiztUIL1Z8Tr;SSL Mode=Require;Trust Server Certificate=true\r\n");

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Patient>().Property(p=>p.isDischarged).HasColumnType("bit");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties()
                                                   .Where(p => p.ClrType == typeof(bool)))
                {
                    property.SetColumnType("boolean");
                }
            }
        }


    }
}
