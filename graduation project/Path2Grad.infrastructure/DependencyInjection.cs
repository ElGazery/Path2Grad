using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Infrastructure.Data;
using Path2Grad.Infrastructure.Repositories;

namespace Path2Grad.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CS")));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISupervisorRepository, SupervisorRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectBankRepository, ProjectBankRepository>();
            services.AddScoped<ITrackRepository, TrackRepository>();
            services.AddScoped<IFieldRepository, FieldRepository>();
            services.AddScoped<ICvRepository, CvRepository>();
            services.AddScoped<IInternshipRepository, InternshipRepository>();
            services.AddScoped<IInternshipWorkFileRepository, InternshipWorkFileRepository>();
            services.AddScoped<IInternshipCertificateRepository, InternshipCertificateRepository>();
            services.AddScoped<IStudentProjectJoinRequestRepository, StudentProjectJoinRequestRepository>();
            services.AddScoped<ISupervisorProjectJoinRequestRepository, SupervisorProjectJoinRequestRepository>();
            services.AddScoped<ISupervisorProjectRepository, SupervisorProjectRepository>();
            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<IProjectFileRepository, ProjectFileRepository>();

            return services;
        }
    }
}
