using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockRoomProject.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public IList<EmployeesItems> EmployeesItems { get; set; }
    }
}
