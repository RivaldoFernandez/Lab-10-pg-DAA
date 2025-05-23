/*******************************************************
*  Program.cs – Configuración de la API Lab-10-Qquelcca
*  ----------------------------------------------------
*  - Registra servicios de capa de infraestructura,
*    aplicación y seguridad.
*  - Configura Swagger con soporte para JWT.
*  - Habilita autenticación / autorización y enrutado.
*******************************************************/

#region using statements
using Lab_10_Qquelcca.Application.Interfaces;
using Lab_10_Qquelcca.Application.Services;
using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Infrastructure.DependencyInjection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;
#endregion

var builder = WebApplication.CreateBuilder(args);

/*------------------------------------------------------
| 1. Servicios MVC (Controladores y Endpoints mínimos)
------------------------------------------------------*/
builder.Services.AddControllers();

/*------------------------------------------------------
| 2. Swagger + JWT ➜ Documentación interactiva
------------------------------------------------------*/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Lab-10-Qquelcca API",
        Version = "v1"
    });

    // Definición del esquema Bearer para envío del token
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In          = ParameterLocation.Header,
        Description = "Escriba:  Bearer {token_jwt}",
        Name        = "Authorization",
        Type        = SecuritySchemeType.ApiKey,
        Scheme      = "Bearer"
    });

    // Requisito global: todos los endpoints aceptan JWT
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

/*------------------------------------------------------
| 3. Servicios de infraestructura / base de datos
------------------------------------------------------*/
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructure(connectionString);

/*------------------------------------------------------
| 4. Servicios de aplicación
------------------------------------------------------*/
builder.Services.AddScoped<IAuthService,   AuthService>();

/*------------------------------------------------------
| 5. Seguridad – Password Hasher para usuarios
------------------------------------------------------*/
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

/*------------------------------------------------------
| 6. Autenticación JWT
------------------------------------------------------*/
var jwtKey      = builder.Configuration["Jwt:Key"];
var jwtIssuer   = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services
    .AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;    // true en producción si usas TLS
        opt.SaveToken            = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer   = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!))
        };
    });

builder.Services.AddAuthorization();

/*------------------------------------------------------
| 7. Construcción de la aplicación
------------------------------------------------------*/
var app = builder.Build();

/*------------------------------------------------------
| 8. Middleware  (pipeline de la app)
------------------------------------------------------*/
if (app.Environment.IsDevelopment())
{
    // Página de excepción detallada + Swagger UI
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab-10-Qquelcca API v1");
        c.RoutePrefix = string.Empty;   // Swagger en raíz
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // ➜ valida JWT
app.UseAuthorization();    // ➜ aplica políticas

/*------------------------------------------------------
| 9. Mapear controladores y arrancar
------------------------------------------------------*/
app.MapControllers();
app.Run();
