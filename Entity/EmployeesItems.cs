using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockRoomProject.Entity
{
    public class EmployeesItems
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
