using Lab_10_Qquelcca.Domain.Interfaces.Unit;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;
using Lab_10_Qquelcca.Infrastructure.Context;
using Lab_10_Qquelcca.Infrastructure.Repositories;
using Lab_10_Qquelcca.Infrastructure.Repositories.Unit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lab_10_Qquelcca.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Registrar DbContext con MySQL (paquete oficial)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(connectionString)); // Aquí va UseMySQL con mayúsculas "SQL"

            // Registrar repositorios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IResponseRepository, ResponseRepository>();

            // Registrar UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}