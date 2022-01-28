using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockRoomProject.Entity;
using StockRoomProject.Interface;

namespace StockRoomProject.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }
    
        public DbSet<EmployeesItems> EmployeesItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeesItems>().HasKey(sc => new { sc.EmployeeId, sc.ItemId });

            modelBuilder.Entity<EmployeesItems>()
                .HasOne<Employee>(sc => sc.Employee)
                .WithMany(s => s.EmployeesItems)
                .HasForeignKey(sc => sc.EmployeeId);


            modelBuilder.Entity<EmployeesItems>()
                .HasOne<Item>(sc => sc.Item)
                .WithMany(s => s.EmployeesItems)
                .HasForeignKey(sc => sc.ItemId);
        }
    }
}
