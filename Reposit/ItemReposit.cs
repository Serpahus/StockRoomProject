using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockRoomProject.Interface;
using StockRoomProject.Models;
using StockRoomProject.Data;
using StockRoomProject.Entity;
using static StockRoomProject.Models.ItemModel;
using Microsoft.EntityFrameworkCore;

namespace StockRoomProject.Reposit
{
    public class ItemReposit : IItem
    {
        private readonly DataContext _context;
        public ItemReposit(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddItem (ItemRequestModel item)
        {
            var _item = new Item
            {
                Name = item.Name,
                Price = item.Price,
                DateCreated = item.DateCreated
            };
            
            var employeesItems = new List<EmployeesItems>();

            if (item.EmployeeId != null)
            {
                foreach (var items in item.EmployeeId)
                {
                    var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == items);

                    if (employee != null)
                    {
                        var employeesItemss = new EmployeesItems
                        {
                            Employee = employee,
                            Item = _item                       
                        };
                        employeesItems.Add(employeesItemss);
                    }

                }

                _item.EmployeesItems = employeesItems;
            }


            await _context.Items.AddAsync(_item);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItem(int Id)
        {
            var item = await _context.Items
                       .Where(x => x.Id == Id)
                       .Include(sc => sc.EmployeesItems)
                       .FirstOrDefaultAsync();
            if (item != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            return false;
        }

        public async Task<ItemModel.ItemResponseModel> GetItem(int Id)
        {
            var query = await _context.Items
                  .Where(x => x.Id == Id)
                  .Select(a => new ItemResponseModel
                  {
                      Id = a.Id,
                      Price = a.Price,
                      Name = a.Name,
                      Employee = _context.EmployeesItems
                                  .Where(x => x.ItemId == a.Id)
                                  .Select(s => new EmployeeModel
                                  {
                                      Name = s.Employee.Name,
                                      
                                      Phone = s.Employee.Phone,
                                  })
                                  .ToList(),
                  }).FirstOrDefaultAsync();
            return query;
           
        }

        public async Task<IEnumerable<ItemModel.ItemResponseModel>> GetItems()
        {
           var query = await _context.Items
                  .Select(a => new ItemResponseModel
                  {
                      Id = a.Id,
                      Price = a.Price,
                      Name = a.Name,
                      Employee = _context.EmployeesItems
                                  .Where(x => x.ItemId == a.Id)
                                  .Select(s => new EmployeeModel
                                  {
                                      Name = s.Employee.Name,
                                      Phone = s.Employee.Phone,
                                  })
                                  .ToList(),
                  }).ToListAsync();



            return query;
        }

        public async Task<bool> UpdateItem(ItemModel.ItemRequestModel item)
        {
            var _item = await _context.Items
                        .Where(x => x.Id == item.ItemId)
                        .FirstOrDefaultAsync();

            _item.Name = item.Name;
            _item.Price = item.Price;

            if (_item != null)
            {
                // employee not mapped to item
                var employeeItems = new List<EmployeesItems>();
                foreach (var element in item.EmployeeId)
                {
                    var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == element);
                    var employeeItem = await _context.EmployeesItems.FirstOrDefaultAsync(x => x.ItemId == item.ItemId && x.EmployeeId == element);;

                    if (employee != null && employeeItem == null)
                    {
                        var newEmploeeItem = new EmployeesItems
                        {
                            Employee = employee,
                            Item = _item
                        };
                        employeeItems.Add(newEmploeeItem);
                    }
                }
                _item.EmployeesItems = employeeItems;
            }
            _context.SaveChanges();
            return true;
        }
    }
}
