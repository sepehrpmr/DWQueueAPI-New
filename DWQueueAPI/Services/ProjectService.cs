using DWQueueAPI.Data;
using DWQueueAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DWQueueAPI.Services
{
    public class ProjectService
    {
        private readonly DWQueueContext _context;


        public ProjectService(DWQueueContext context)
        {
            _context = context;
        }



        public async Task UpdateProjectAsync(Projects project)
        {
            var existingProject = await _context.Projectss.FirstOrDefaultAsync(p => p.ProjectID == project.ProjectID);
            if (existingProject != null)
            {
                existingProject.ProjectName = project.ProjectName;
                existingProject.StartDate = project.StartDate;
                existingProject.EndDate = project.EndDate;
                existingProject.Budget = project.Budget;
                await _context.SaveChangesAsync();
            }
        }





        public async Task DeleteProjectAsync(int id)
        {
            var project = await _context.Projectss.FirstOrDefaultAsync(p => p.ProjectID == id);
            if (project != null)
            {
                _context.Projectss.Remove(project);
                await _context.SaveChangesAsync();
            }
        }





        public async Task AddProjectAsync(Projects project)
        {
            await _context.Projectss.AddAsync(project);
            await _context.SaveChangesAsync();
        }





        public async Task<Projects> GetProjectByIDAsync(int id)
        {
            return await _context.Projectss.FirstOrDefaultAsync(p => p.ProjectID == id);
        }



        public async Task<List<Projects>> GetAllProjectsAsync()
        {
            return await _context.Projectss.ToListAsync();
        }



    }
}
