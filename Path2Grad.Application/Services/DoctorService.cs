using Path2Grad.Application.Dtos;
using Path2Grad.Application.Helpers;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Application.Interfaces.Services;
using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ISupervisorRepository _supervisorRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly ISupervisorProjectRepository _supervisorProjectRepo;
        private readonly ISupervisorProjectJoinRequestRepository _joinRequestRepo;
        private readonly IRepository<ProjectRequirement> _requirementRepo;
        private readonly IProjectFileRepository _fileRepo;
        private readonly IStudentRepository _studentRepo;

        public DoctorService(
            ISupervisorRepository supervisorRepo,
            IProjectRepository projectRepo,
            ISupervisorProjectRepository supervisorProjectRepo,
            ISupervisorProjectJoinRequestRepository joinRequestRepo,
            IRepository<ProjectRequirement> requirementRepo,
            IProjectFileRepository fileRepo,
            IStudentRepository studentRepo)
        {
            _supervisorRepo = supervisorRepo;
            _projectRepo = projectRepo;
            _supervisorProjectRepo = supervisorProjectRepo;
            _joinRequestRepo = joinRequestRepo;
            _requirementRepo = requirementRepo;
            _fileRepo = fileRepo;
            _studentRepo = studentRepo;
        }

        public async Task<object?> GetProfileAsync(string email)
        {
            return await _supervisorRepo.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<object>> GetProjectsAsync(string email)
        {
            var doctor = await _supervisorRepo.GetByEmailAsync(email);
            if (doctor == null) return Enumerable.Empty<object>();

            var projectIds = await _supervisorProjectRepo.GetProjectIdsBySupervisorIdAsync(doctor.SupervisorId);
            var projects = await _projectRepo.GetAllProjectsWithDetailsAsync();
            var filteredProjects = projects.Where(p => projectIds.Contains(p.ProjectId));

            return filteredProjects.Select(p => new
            {
                p.ProjectId,
                p.ProjectName,
                p.Description,
                Students = p.Students.Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.StudentName,
                    Pic = s.Pic
                }).ToList(),
                TeachingAssistants = p.SupervisorProjects
                    .Where(sp => sp.Supervisor.Position == "TeachingAssistant")
                    .Select(sp => new
                    {
                        TeachingAssistanId = sp.Supervisor.SupervisorId,
                        TeacahingAssistanName = sp.Supervisor.SupervisorName,
                        TeachingAssistantPic = sp.Supervisor.Pic
                    }).ToList()
            }).ToList();
        }

        public async Task<object?> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepo.GetProjectWithDetailsAsync(id);
            if (project == null) return null;

            return new
            {
                project.ProjectId,
                project.ProjectName,
                project.ProjectFields,
                project.Description,
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

        public async Task AddRequirementAsync(ProjectRequirementCreateDto dto)
        {
            var projectRequirement = ProjectRequirementHelper.ToEntity(dto);
            await _requirementRepo.AddAsync(projectRequirement);
            await _requirementRepo.SaveChangesAsync();
        }

        public async Task DeleteRequirementAsync(int requirementId)
        {
            var requirement = await _requirementRepo.GetByIdAsync(requirementId);
            if (requirement != null)
            {
                _requirementRepo.Delete(requirement);
                await _requirementRepo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<object>> GetProjectFilesAsync(string email)
        {
            var doctor = await _supervisorRepo.GetByEmailAsync(email);
            if (doctor == null) return Enumerable.Empty<object>();

            var projectIds = await _supervisorProjectRepo.GetProjectIdsBySupervisorIdAsync(doctor.SupervisorId);
            var projectFiles = await _fileRepo.GetByProjectIdsAsync(projectIds);

            return projectFiles.Select(pf => new
            {
                pf.FileName,
                pf.FileContent
            }).ToList();
        }

        public async Task<IEnumerable<object>> GetProjectRequestsAsync(string email)
        {
            var doctor = await _supervisorRepo.GetByEmailAsync(email);
            if (doctor == null) return Enumerable.Empty<object>();

            var allRequests = await _joinRequestRepo.GetBySupervisorIdAsync(doctor.SupervisorId);
            var result = new List<object>();

            foreach (var request in allRequests)
            {
                var studentName = (await _studentRepo.GetByIdAsync(request.StudentId))?.StudentName;
                result.Add(new
                {
                    RequestId = request.RequestId,
                    SenderPic = request.Supervisor?.Pic,
                    SenderName = studentName,
                    ProjectName = request.Project?.ProjectName,
                    projectId = request.ProjectId
                });
            }

            return result;
        }

        public async Task<string> HandleStatusRequestAsync(string email, SupervisorStatusRequestDto dto)
        {
            var doctor = await _supervisorRepo.GetByEmailAsync(email);
            if (doctor == null) return "Doctor not found";

            var request = await _joinRequestRepo.GetByIdAsync(dto.RequestId);
            if (request == null) return "Request not found.";

            if (dto.Status == "Remove")
            {
                _joinRequestRepo.Delete(request);
            }
            else if (dto.Status == "Accept")
            {
                var supervisorProject = new SupervisorProject
                {
                    ProjectId = dto.ProjectId,
                    SupervisorId = doctor.SupervisorId
                };
                await _supervisorProjectRepo.AddAsync(supervisorProject);
                _joinRequestRepo.Delete(request);
            }
            else
            {
                return "Status not correct";
            }

            await _joinRequestRepo.SaveChangesAsync();
            return "The project was successfully accepted";
        }
    }
}
