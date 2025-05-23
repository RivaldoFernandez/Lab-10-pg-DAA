using Lab_10_Qquelcca.Application.Interfaces;
using Lab_10_Qquelcca.Application.Services;
using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;
using Lab_10_Qquelcca.Domain.Interfaces.Unit;
using Lab_10_Qquelcca.Infrastructure.Context;
using Lab_10_Qquelcca.Infrastructure.Repositories;
using Lab_10_Qquelcca.Infrastructure.Repositories.Unit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lab_10_Qquelcca.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Clase estática encargada de registrar todos los servicios de la capa de infraestructura
    /// en el contenedor de dependencias. Esta clase organiza y centraliza la configuración
    /// de acceso a datos y lógica de infraestructura.
    /// </summary>
    public static class InfrastructureServiceRegistration
    {
        /// <summary>
        /// Método de extensión para registrar los servicios de infraestructura, incluyendo
        /// DbContext, repositorios, servicios de aplicación, Unit of Work y hasheado de contraseñas.
        /// </summary>
        /// <param name="services">El contenedor de servicios (IServiceCollection)</param>
        /// <param name="connectionString">Cadena de conexión a la base de datos MySQL</param>
        /// <returns>IServiceCollection con todos los servicios de infraestructura registrados</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // 1. Registrar el ApplicationDbContext con soporte para MySQL.
            // Este DbContext manejará las operaciones de base de datos usando EF Core.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(connectionString)); // Requiere el paquete Pomelo.EntityFrameworkCore.MySql

            // 2. Registrar todos los repositorios personalizados.
            // Cada repositorio encapsula la lógica de acceso a datos para una entidad del dominio.
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IResponseRepository, ResponseRepository>();

            // 3. Registrar los servicios de aplicación relacionados a cada entidad.
            // Estos servicios definen la lógica de negocio y dependen de los repositorios.
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITicketService, TicketService>();
    

            // 4. Registrar el patrón Unit of Work para gestionar múltiples transacciones
            // dentro de una misma operación de manera coherente y eficiente.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 5. Registrar el servicio de hasheado de contraseñas con Identity.
            // Esto permite gestionar contraseñas seguras para los usuarios.
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddScoped<ResponseService>();
            // Devuelve la colección de servicios configurados
            return services;
        }
    }
}
