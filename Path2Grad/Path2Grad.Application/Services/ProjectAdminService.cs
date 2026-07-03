using Path2Grad.Application.Dtos;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Application.Interfaces.Services;
using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Services
{
    public class ProjectAdminService : IProjectAdminService
    {
        private readonly IRepository<ProjectsAdmin> _adminRepo;
        private readonly IProjectRepository _projectRepo;

        public ProjectAdminService(
            IRepository<ProjectsAdmin> adminRepo,
            IProjectRepository projectRepo)
        {
            _adminRepo = adminRepo;
            _projectRepo = projectRepo;
        }

        public async Task<object?> GetProfileAsync(string email)
        {
            var admins = await _adminRepo.FindAsync(d => d.AdminEmail == email);
            return admins.FirstOrDefault();
        }

        public async Task<IEnumerable<object>> GetAllProjectsAsync()
        {
            var projects = await _projectRepo.GetAllProjectsWithDetailsAsync();
            return projects.Select(p => new
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                Description = p.Description,
                Students = p.Students.Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    Pic = s.Pic
                }).ToList(),
                Supervisors = p.SupervisorProjects.Select(sp => new SupervisorDto
                {
                    SupervisorId = sp.Supervisor.SupervisorId,
                    SupervisorName = sp.Supervisor.SupervisorName,
                    Pic = sp.Supervisor.Pic
                }).ToList()
            }).ToList();
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await _projectRepo.GetByIdAsync(projectId);
            if (project != null)
            {
                _projectRepo.Delete(project);
                await _projectRepo.SaveChangesAsync();
            }
        }

        public async Task<object?> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepo.GetProjectWithDetailsAsync(id);
            if (project == null) return null;

            return new
            {
                project.ProjectId,
                project.ProjectName,
                project.Description,
                project.ProjectFields,
                project.NumberOfTeam,
                Students = project.Students.Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    Pic = s.Pic
                }).ToList(),
                Supervisors = project.SupervisorProjects.Select(sp => new SupervisorDto
                {
                    SupervisorId = sp.Supervisor.SupervisorId,
                    SupervisorName = sp.Supervisor.SupervisorName,
                    Pic = sp.Supervisor.Pic
                }).ToList()
            };
        }
    }
}
