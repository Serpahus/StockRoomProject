using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockRoomProject.Models
{
    public class EmployeeModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
    public class EmployeeRequestModel : EmployeeModel
    {
        public int EmployeeId { get; set; }
        public List<int> ItemId { get; set; }
    }
    public class EmployeeResponseModel : EmployeeModel
    {
        public int Id { get; set; }
        public List<ItemModel> Item { get; set; }
    }
}

