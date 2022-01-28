using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockRoomProject.Entity
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int Price { get; set; }

        public IList<EmployeesItems> EmployeesItems { get; set; }
    }
}
