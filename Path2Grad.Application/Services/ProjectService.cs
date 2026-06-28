using Path2Grad.Application.Dtos;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Application.Interfaces.Services;
using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectBankRepository _projectBankRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IFieldRepository _fieldRepo;

        public ProjectService(
            IProjectRepository projectRepo,
            IProjectBankRepository projectBankRepo,
            IStudentRepository studentRepo,
            IFieldRepository fieldRepo)
        {
            _projectRepo = projectRepo;
            _projectBankRepo = projectBankRepo;
            _studentRepo = studentRepo;
            _fieldRepo = fieldRepo;
        }

        public async Task<IEnumerable<object>> GetProjectBankAsync()
        {
            var projects = await _projectBankRepo.GetAllAsync();
            return projects.Select(e => new
            {
                ProjectId = e.ProjectBankId,
                ProjectName = e.ProjectName,
                ProjectDescripition = e.Description
            }).ToList();
        }

        public async Task<object?> GetProjectBankByIdAsync(int id)
        {
            var project = await _projectBankRepo.GetProjectWithFieldsAsync(id);
            if (project == null) return null;

            return new
            {
                ProjectId = project.ProjectBankId,
                ProjectName = project.ProjectName,
                ProjectDescripition = project.Description,
                project.ProjectSpecification,
                projectFields = project.ProjectsBankProjectFields?
                    .Select(p => new
                    {
                        p.ProjectFieldId,
                        p.ProjectField
                    }).ToList()
            };
        }

        public async Task<string> CustomizeProjectAsync(string email, ProjectDto projectDto)
        {
            var allStudents = await _studentRepo.FindAsync(s => s.StudentEmail == email);
            var student = allStudents.FirstOrDefault();
            if (student == null)
                return "Student not found or not authenticated";

            var project = new Project
            {
                ProjectName = projectDto.ProjectName,
                Description = projectDto.Description,
                NumberOfTeam = projectDto.NumberOfTeam,
            };

            await _projectRepo.AddAsync(project);

            var fieldNames = projectDto.Fields.Select(f => f.FieldName).ToList();
            var fields = await _fieldRepo.GetFieldsByNamesAsync(fieldNames);

            foreach (var field in fields)
            {
                project.ProjectFields.Add(new ProjectField
                {
                    ProjectId = project.ProjectId,
                    FieldId = field.FieldId
                });
            }

            student.ProjectId = project.ProjectId;
            _studentRepo.Update(student);

            await _projectRepo.SaveChangesAsync();

            return "Project created successfully. Start to add supervisors and students.";
        }
    }
}
