using DWQueueAPI.Data;
using DWQueueAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DWQueueAPI.Sevices
{
    public class EmployeeService
    {
        // not async for now, will look into making this async later on, not sure if it will be worth it or not but it will be good practice for me to learn how to do it properly
        // not sure if this is the right way to do it but it works for now, will look into better ways to do this later on
        private readonly DWQueueContext _context;

        public EmployeeService(DWQueueContext context)
        {
            _context = context;
        }

       
        

        public async Task UpdateEmployeeAsync(Employees employee)
        {
            var existingEmployee = await _context.Employeess.FirstOrDefaultAsync(e => e.EmployeeID == employee.EmployeeID);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Position = employee.Position;
                existingEmployee.HireDate = employee.HireDate;
                existingEmployee.DepartmentID = employee.DepartmentID;
                await _context.SaveChangesAsync();
            }
        }



        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employeess.FirstOrDefaultAsync(e => e.EmployeeID == id);
            if (employee != null)
            {
                _context.Employeess.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }



        public async Task AddEmployeeAsync(Employees employee)
        {
            await _context.Employeess.AddAsync(employee);
            await _context.SaveChangesAsync();
        }


        public async Task<Employees> GetEmployeeByIDAsync(int id)
        {
            return await _context.Employeess.FirstOrDefaultAsync(e => e.EmployeeID == id);
        }



        public async Task<List<Employees>> GetAllEmployeesAsync()
        {
            return await _context.Employeess.ToListAsync();
        }


       
    }
}
