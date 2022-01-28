using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockRoomProject.Models
{
    public class ItemModel
    {
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
        public int Price { get; set; }
        public class ItemRequestModel : ItemModel
        {
            public int ItemId { get; set; }
            public List<int> EmployeeId { get; set; }
        }
        public class ItemResponseModel : ItemModel
        {
            public int Id { get; set; }
            public List<EmployeeModel> Employee { get; set; }
        }
    }
}
