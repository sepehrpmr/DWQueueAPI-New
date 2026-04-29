using DWQueueAPI.Data;
using DWQueueAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DWQueueAPI.Services
{
    public class DepartmentService
    {
        private readonly DWQueueContext _context;

        public DepartmentService(DWQueueContext context)
        {
            _context = context;
        }



        public async Task UpdateDepartmentAsync(Departments department)
        {
            var existingDepartment = await _context.Departmentss.FirstOrDefaultAsync(d => d.DepartmentID == department.DepartmentID);
            if (existingDepartment != null)
            {
                existingDepartment.DepartmentName = department.DepartmentName;
                await _context.SaveChangesAsync();
            }
        }



        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departmentss.FirstOrDefaultAsync(d => d.DepartmentID == id);
            if (department != null)
            {
                _context.Departmentss.Remove(department);
                await _context.SaveChangesAsync();
            }
        }


        public async Task AddDepartmentAsync(Departments department)
        {
            await _context.Departmentss.AddAsync(department);
            await _context.SaveChangesAsync();
        }


        public async Task<Departments> GetDepartmentByIDAsync(int id)
        {
            return await _context.Departmentss.FirstOrDefaultAsync(d => d.DepartmentID == id);
        }



        public async Task<List<Departments>> GetAllDepartmentsAsync()
        {
            return await _context.Departmentss.ToListAsync();
        }
    }
}
