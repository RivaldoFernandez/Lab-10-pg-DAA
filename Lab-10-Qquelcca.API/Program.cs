//
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using Lab_10_Qquelcca.Infrastructure.Context;
// using Microsoft.EntityFrameworkCore;
//
//
// var builder = WebApplication.CreateBuilder(args);
//
// // 1. Configuración básica de la aplicación
// builder.Services.AddControllersWithViews(); // Agrega soporte a controladores y vistas
// builder.Services.AddEndpointsApiExplorer(); // Soporte para Endpoints (Swagger)
// builder.Services.AddSwaggerGen(); // Documentación Swagger
//
// // Agregar tu DbContext
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseMySQL(
//         builder.Configuration.GetConnectionString("DefaultConnection"))
// );
//
//
//
// // 2. Configuración JWT (AGREGADO NUEVO)
// var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>(); // Obtiene sección del appsettings.json
// builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings")); // Inyecta configuración en servicios
//
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Define el esquema de autenticación
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//     options.RequireHttpsMetadata = false; // No requiere HTTPS para desarrollo
//     options.SaveToken = true; // Guarda el token en el contexto de la solicitud
//
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true, // Valida el emisor del token
//         ValidIssuer = jwtSettings.Issuer, // Emisor definido en appsettings.json
//
//         ValidateAudience = true, // Valida el público
//         ValidAudience = jwtSettings.Audience, // Audiencia definida en appsettings.json
//
//         ValidateLifetime = true, // Valida la vigencia
//         ValidateIssuerSigningKey = true, // Valida firma del emisor
//
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)) // Llave secreta
//     };
// });
//
// // 3. Autorización (AGREGADO NUEVO)
// builder.Services.AddAuthorization();
//
// // // Passworhasher
// // builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
// //
// // builder.Services.AddScoped<IUserRepository, UserRepository>();  // repositorio
// // builder.Services.AddScoped<IUserService, UserService>();        // lógica de usuario
// //
// // builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
// // builder.Services.AddScoped<IUserRepository, UserRepository>();
// //
//
//
//
// // 4. Construcción de la aplicación
// var app = builder.Build();
//
// // 5. Middleware pipeline
// if (app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error"); // Manejo de errores
//     app.UseSwagger(); // Habilita Swagger en entorno dev
//     app.UseSwaggerUI(options =>
//     {
//         options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
//         options.RoutePrefix = string.Empty;
//     });
// }
//
// app.UseHttpsRedirection(); // Redirige HTTP  HTTPS
// app.UseStaticFiles();      // Habilita archivos estáticos como CSS o JS
// app.UseRouting();          // Habilita enrutamiento
//
// // 6. Middleware de autenticación y autorización (AGREGADO NUEVO)
// app.UseAuthentication(); // IMPORTANTE: debe ir antes que Authorization
// app.UseAuthorization();  // Aplica políticas de autorización
//
// // 7. Configuración de rutas
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
//
// // 8. Ejecuta la app
// app.Run();




using Lab_10_Qquelcca.Application.Interfaces;
using Lab_10_Qquelcca.Application.Services;
using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces;
using Lab_10_Qquelcca.Infrastructure.Repositories;
using Lab_10_Qquelcca.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios a la contenedor de dependencias
builder.Services.AddControllers();

// 2. Agregar Swagger (con soporte para JWT)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Lab-10-Qquelcca API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese su token JWT en este formato: Bearer {su token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// 3. Inyectar PasswordHasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// 4. Leer cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 5. Registrar infraestructura y dependencias personalizadas
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
// 6. Registrar servicios de aplicación
builder.Services.AddScoped<IAuthService, AuthService>();

// 7. Configurar autenticación JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)) // Aquí fallaba
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// 8. Configurar middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab-10-Qquelcca API v1");
        options.RoutePrefix = string.Empty;
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

app.UseAuthentication(); // JWT Auth
app.UseAuthorization();

app.MapControllers();

app.Run();
