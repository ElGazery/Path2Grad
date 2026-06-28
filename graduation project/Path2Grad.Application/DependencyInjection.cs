using Microsoft.Extensions.DependencyInjection;
using Path2Grad.Application.Interfaces.Services;
using Path2Grad.Application.Services;

namespace Path2Grad.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<ITeachingAssistantService, TeachingAssistantService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectAdminService, ProjectAdminService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IInternshipService, InternshipService>();
            services.AddScoped<ICVService, CVService>();
            services.AddScoped<IDownListService, DownListService>();
            services.AddScoped<IRecommendationService, RecommendationService>();

            return services;
        }
    }
}
