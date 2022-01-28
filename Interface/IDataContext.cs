using Microsoft.EntityFrameworkCore;
using StockRoomProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace StockRoomProject.Interface
{
   public interface IDataContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<EmployeesItems> EmployeesItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
