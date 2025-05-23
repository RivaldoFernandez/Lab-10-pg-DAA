using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Lab_10_Qquelcca.Configuration;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

         // Registre el servicio IClientContextProvider para acceder a los headers de cada request
         // services.AddScoped<IClientContextProvider, ClientContextProvider>();
         //
         // // Registro de servicios de Infraestructura
         // services.AddInfrastructureServices(configuration);

         // Configuración de autenticación JWT
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidIssuer = "Languagebridgesolutions.com",
                     ValidAudience = "Languagebridgesolutions.com",
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secretkey"]))
                 };
             });

         // habilitar controladores
         services.AddControllers();

         // Habilitar Swagger
         services.AddEndpointsApiExplorer();
         services.AddSwaggerGen(options =>
         {
             // Puedes personalizar las opciones de Swagger aqui, por ejemplo, agregar una descripción o la versión de la API
             options.SwaggerDoc(
                 name: "v1",
                 new OpenApiInfo
                 {
                     Title = "Mi API",
                     Version = "v1",
                     Description = "API para gestionar recursos."
                 });
         });

         return services;
    }


}
