using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockRoomProject.Models;
using StockRoomProject.Entity;
using static StockRoomProject.Models.ItemModel;

namespace StockRoomProject.Interface
{
   public interface IItem
    {
        Task<bool> AddItem (ItemRequestModel item);
        Task<IEnumerable<ItemResponseModel>> GetItems();
        Task<ItemResponseModel> GetItem(int Id);
        Task<bool> DeleteItem(int Id);
        Task<bool> UpdateItem(ItemRequestModel item);
    }
}
