using System.Text;
using CrudProducts.Properties.Infrastructure.Data;
using CrudProducts.src.Application.Services.Implements;
using CrudProducts.src.Application.Services.Interfaces;
using CrudProducts.src.Infrastructure.Data;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IProductService, ProductService>();

//Conexión a base de datos de módulo de usuarios (MySQL)
var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
builder.Services.AddDbContextPool<DataContext>(options =>
{
    options.UseMySql(Env.GetString("MYSQL_CONNECTION"), serverVersion,
        mySqlOptions => 
        {
            mySqlOptions.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
            mySqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            );
            mySqlOptions.CommandTimeout(120);
        });
}, poolSize: 200);


//Configuración de middleware de autenticación
builder.Services.AddAuthentication( options => {

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer( options => {

    options.TokenValidationParameters = new TokenValidationParameters (){
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JWT_SECRET"))),
        ValidateLifetime = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero 
    };
});


var app = builder.Build();

//Llamado al dataseeder
using (var scope = app.Services.CreateScope())
{
    await DataSeeder.Initialize(scope.ServiceProvider);
}

app.UseHttpsRedirection();
app.UseSwaggerUI( c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty;
});
app.UseSwagger();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
