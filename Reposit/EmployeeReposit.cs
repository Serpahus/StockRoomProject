using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockRoomProject.Interface;
using StockRoomProject.Models;
using StockRoomProject.Data;
using StockRoomProject.Entity;
using Microsoft.EntityFrameworkCore;
using static StockRoomProject.Models.EmployeeModel;

namespace StockRoomProject.Reposit
{
    public class EmployeeReposit : IEmployee
    {
        private readonly DataContext _context;
        public EmployeeReposit(DataContext context)
        {
            _context = context;
        }

        public async Task <bool> AddEmployee(EmployeeRequestModel employee)
        {
            var _employee = new Employee
            {
                
                Name = employee.Name,
                Phone = employee.Phone
            };

            var employeeItems = new List<EmployeesItems>();

            if (employee.ItemId != null)
            {
                foreach (var item in employee.ItemId)
                {
                    var items = await _context.Items.FirstOrDefaultAsync(x => x.Id == item);
                    if (items != null)
                    {
                        var employeeItem = new EmployeesItems
                        {
                            Item = items ,
                            Employee = _employee
                        };
                        employeeItems.Add(employeeItem);
                    }
                }
                _employee.EmployeesItems = employeeItems;
            }
            await _context.Employees.AddAsync(_employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task <bool> DeleteEmployee(int Id)
        {
            var employee = await _context.Employees
                        .Where(x => x.Id == Id)
                        .Include(sc => sc.EmployeesItems)
                        .FirstOrDefaultAsync();

            _context.Remove(employee);
            _context.SaveChanges();
            return true;
        }

        public async Task<EmployeeResponseModel> GetEmployee(int Id)
        {
            var query = await _context.Employees
                 .Where(x => x.Id == Id)
                 .Select(s => new EmployeeResponseModel
                 {
                     Name = s.Name,
                     Phone = s.Phone,
                     Item = _context.EmployeesItems
                                 .Where(x => x.EmployeeId == s.Id)
                                 .Select(c => new ItemModel
                                 {
                                     Price = c.Item.Price,
                                     Name = c.Item.Name,
                                 })
                                 .ToList(),
                 }).FirstOrDefaultAsync();

            return query;
        }

        public async Task<IEnumerable<EmployeeResponseModel>> GetEmployees()
        {
            var query = await _context.Employees
                  .Select(a => new EmployeeResponseModel
                  {
                      Id = a.Id,
                      Name = a.Name,
                      Phone = a.Phone,
                      Item = a.EmployeesItems.Where(x => x.EmployeeId == a.Id)
                                  .Select(c => new ItemModel
                                  {
                                      Price = c.Item.Price,
                                      Name = c.Item.Name,
                                  })
                                  .ToList(),
                  }).ToListAsync();

            return query;
        }
        
        public async Task <bool> UpdateEmployee(EmployeeRequestModel employee)
        {
            var _employee = await _context.Employees
                       .Where(x => x.Id == employee.EmployeeId)
                       .FirstOrDefaultAsync();

            _employee.Name = employee.Name;

            _employee.Phone = employee.Phone;

            if (_employee != null)
            {
                // Employee not mapped to item
                var employeeItems = new List<EmployeesItems>();
                foreach (var item in employee.ItemId)
                {
                    var items = await _context.Items.FirstOrDefaultAsync(x => x.Id == item);
                    var employeeItem = await _context.EmployeesItems.FirstOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId && x.ItemId == item);

                    if (items != null && employeeItem == null)
                    {
                        var newEmployeeItem = new EmployeesItems
                        {
                            Employee = _employee,
                            Item = items
                        };
                        employeeItems.Add(newEmployeeItem);
                    }
                }
                _employee.EmployeesItems = employeeItems;
            }
           
            _context.SaveChanges();
            return true;
        }
    }
}
