using StockRoomProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StockRoomProject.Models.EmployeeModel;



namespace StockRoomProject.Interface
{
    public interface IEmployee
    {
        Task<bool> AddEmployee (EmployeeRequestModel employee);
        Task<IEnumerable<EmployeeResponseModel>> GetEmployees();
        Task<EmployeeResponseModel> GetEmployee(int Id);
        Task<bool> DeleteEmployee(int Id);
        Task<bool> UpdateEmployee(EmployeeRequestModel employee);
    }
}
