using BazaarOnline.API.Hubs;
using BazaarOnline.Application.Middlewares;
using BazaarOnline.Infra.Data.Contexts;
using BazaarOnline.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DotNetEnv
DotNetEnv.Env.Load();

// SignalR
builder.Services.AddSignalR();

// CORS
//string NextJsOrigin = "NextJS";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(NextJsOrigin,
//        builder => { builder.WithOrigins("localhost:3000").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
//});

string AllowAllOrigin = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAllOrigin,
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// XML Content Type
builder.Services.AddControllers();
// Access to HttpContext in DI
// https://stackoverflow.com/a/56388997/14034832
// builder.Services.AddHttpContextAccessor();

// SQL DB
builder.Services.AddDbContext<BazaarDbContext>(options => options.UseSqlServer(DotNetEnv.Env.GetString("CONNECTION_STRING"),
    x =>
        x.MigrationsHistoryTable(
            HistoryRepository.DefaultTableName,
            schema: System.Environment.GetEnvironmentVariable("DATABASE_SCHEMA_NAME"))));

// POSTGRES DB
//builder.Services.AddDbContext<BazaarPostgresDbContext>(options =>
//    options.UseNpgsql(System.Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING")));

// Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

// Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(System.Environment.GetEnvironmentVariable("JWT__SIGNKEY"))),
            TokenDecryptionKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(System.Environment.GetEnvironmentVariable("JWT__ENCRYPTKEY")))
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "Bazaar API",
        Description = "Main API Documentation of Bazaar API",
        License = new OpenApiLicense { Name = "MIT" },
        Contact = new OpenApiContact
        {
            Email = "matin.khaleghi.nezhad@gmail.com",
            Name = "Matin Khaleghi"
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

DependencyContainer.RegisterService(builder.Services);

var app = builder.Build();

if (Environment.GetEnvironmentVariable("IS_DEVELOPMENT") != "1")
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<BazaarDbContext>();
        dbContext.Database.Migrate();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/V1/swagger.json", "V1"); });
}

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US")
});
app.UseCors(AllowAllOrigin);
//app.UseCors(NextJsOrigin);
app.UseStaticFiles();

app.UseMiddleware<SignalRAuthenticationFix>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/ws/chat");
app.Run();