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



        public async Task UpdateDepartmentAsync(Departments department , CancellationToken cancellationToken)
        {
            var existingDepartment = await _context.Departmentss.FirstOrDefaultAsync(d => d.DepartmentID == department.DepartmentID , cancellationToken);
            if (existingDepartment != null)
            {
                existingDepartment.DepartmentName = department.DepartmentName;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }



        public async Task DeleteDepartmentAsync(int id , CancellationToken cancellationToken)
        {
            var department = await _context.Departmentss.FirstOrDefaultAsync(d => d.DepartmentID == id, cancellationToken);
            if (department != null)
            {
                _context.Departmentss.Remove(department);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }


        public async Task AddDepartmentAsync(Departments department , CancellationToken cancellationToken )
        {
            await _context.Departmentss.AddAsync(department , cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<Departments> GetDepartmentByIDAsync(int id , CancellationToken cancellationToken)
        {
            return await _context.Departmentss.FirstOrDefaultAsync(d => d.DepartmentID == id , cancellationToken);
        }



        public async Task<List<Departments>> GetAllDepartmentsAsync(CancellationToken cancellationToken)
        {
            return await _context.Departmentss.ToListAsync(cancellationToken);
        }
    }
}
