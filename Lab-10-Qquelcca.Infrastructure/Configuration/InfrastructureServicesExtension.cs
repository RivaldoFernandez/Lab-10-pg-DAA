// using Lab_10_Qquelcca.Infrastructure.Context;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace Lab_10_Qquelcca.Infrastructure.Configuration;
//
// public class InfrastructureServicesExtension
// {
//     public static IServiceCollection AddInfrastructureServices(
//         this IServiceCollection services, 
//         IConfiguration configuration) 
//     {
//         // DataBase Connection
//         services.AddDbContext<ApplicationDbContext>((serviceProvider, options) => 
//         {
//             var connectionString = configuration.GetConnectionString("DefaultConnection");
//             options.UseNpgsql(connectionString);
//         });
//
//         // Services Register
//         services.AddTransient<IUnitOfWork, UnitOfWork>();
//         services.AddScoped<IAuthService, AuthService>();
//         services.AddScoped<IFileService, FileService>();
//         services.AddScoped<IUploadFileToAzureStorageService, UploadFileToAzureStorageService>();
//         services.AddScoped<IActivityService, ActivityService>();
//
//         return services;
//     }
// }
//
//

