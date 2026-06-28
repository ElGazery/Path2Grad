using Path2Grad.Application.Dtos;
using Path2Grad.Application.Helpers;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Application.Interfaces.Services;
using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IStudentProjectJoinRequestRepository _joinRequestRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectTaskRepository _taskRepo;
        private readonly IProjectFileRepository _fileRepo;
        private readonly ISupervisorProjectJoinRequestRepository _supervisorJoinRequestRepo;

        public StudentService(
            IStudentRepository studentRepo,
            IStudentProjectJoinRequestRepository joinRequestRepo,
            IProjectRepository projectRepo,
            IProjectTaskRepository taskRepo,
            IProjectFileRepository fileRepo,
            ISupervisorProjectJoinRequestRepository supervisorJoinRequestRepo)
        {
            _studentRepo = studentRepo;
            _joinRequestRepo = joinRequestRepo;
            _projectRepo = projectRepo;
            _taskRepo = taskRepo;
            _fileRepo = fileRepo;
            _supervisorJoinRequestRepo = supervisorJoinRequestRepo;
        }

        public async Task<Student?> GetProfileAsync(string email)
        {
            return await _studentRepo.GetByEmailAsync(email);
        }

        public async Task SendProjectRequestAsync(int senderId, int receiverId, int? projectId)
        {
            var request = new StudentProjectJoinRequest
            {
                SenderId = senderId,
                StudentId = receiverId,
                ProjectId = projectId
            };
            await _joinRequestRepo.AddAsync(request);
            await _joinRequestRepo.SaveChangesAsync();
        }

        public async Task<object> GetProjectRequestsAsync(int studentId, int? currentProjectId)
        {
            if (currentProjectId != null)
                return "You are joined in a project..";

            var requests = await _joinRequestRepo.GetByStudentIdAsync(studentId);
            return requests.Select(e => new
            {
                Requestid = e.RequestId,
                SenderPic = e.Sender.Pic,
                SenderName = e.Sender.StudentName,
                ProjectName = e.Project?.ProjectName
            }).ToList();
        }

        public async Task<Student?> HandleStatusRequestAsync(int studentId, int requestId, string status)
        {
            var request = await _joinRequestRepo.GetByIdWithDetailsAsync(requestId);
            if (request == null) return null;

            if (status == "Remove")
            {
                _joinRequestRepo.Delete(request);
            }
            else if (status == "Accept")
            {
                var student = await _studentRepo.GetByIdAsync(studentId);
                if (student != null)
                {
                    student.ProjectId = request.ProjectId;
                    _studentRepo.Update(student);
                }
                _joinRequestRepo.Delete(request);
            }
            else
            {
                return null;
            }

            await _joinRequestRepo.SaveChangesAsync();
            return await _studentRepo.GetByIdAsync(studentId);
        }

        public async Task<object?> GetProjectAsync(int studentId)
        {
            var student = await _studentRepo.GetByIdAsync(studentId);
            if (student?.ProjectId == null) return null;

            var project = await _projectRepo.GetProjectWithDetailsAsync(student.ProjectId.Value);
            if (project == null) return null;

            return new
            {
                ProjectRequirements = project.Requirements?.Select(x => new
                {
                    RequirementName = x.RequirementName,
                    Pdf = x.PdfContent
                }).ToList(),

                ProjectFiles = project.projectFiles?.Select(f => new
                {
                    FileName = f.FileName,
                    FileContent = f.FileContent
                }).ToList(),

                ProjectTasks = project.Tasks?.Select(t => new
                {
                    TaskName = t.TaskName,
                    AssignedToName = t.Student?.StudentName,
                    AssignedToPic = t.Student?.Pic,
                    Deadline = t.Deadline,
                    Status = t.Status
                }).ToList()
            };
        }

        public async Task AddTaskAsync(AddTaskDto dto)
        {
            var task = new ProjectTask
            {
                TaskName = dto.TaskName,
                StudentId = dto.StudentId,
                ProjectId = dto.ProjectId,
                Deadline = dto.Deadline
            };
            await _taskRepo.AddAsync(task);
            await _taskRepo.SaveChangesAsync();
        }

        public async Task UploadFileAsync(int studentId, ProjectFileDto file)
        {
            var pdf = IFormToByteHelper.ConvertToBytes(file.File);
            var projectFile = new ProjectFile
            {
                FileContent = pdf,
                ProjectId = file.ProjectId
            };
            await _fileRepo.AddAsync(projectFile);
            await _fileRepo.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task != null)
            {
                _taskRepo.Delete(task);
                await _taskRepo.SaveChangesAsync();
            }
        }

        public async Task SendSupervisorRequestAsync(int studentId, int supervisorId, int? projectId)
        {
            var request = new SupervisorProjectJoinRequest
            {
                StudentId = studentId,
                SupervisorId = supervisorId,
                ProjectId = projectId
            };
            await _supervisorJoinRequestRepo.AddAsync(request);
            await _supervisorJoinRequestRepo.SaveChangesAsync();
        }
    }
}
