using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Serilog;
using WarehouseManagementSystemApi.Data;
using WarehouseManagementSystemApi.Extensions;
using WarehouseManagementSystemApi.MiddleWares;
using WarehouseManagementSystemApi.MiddleWares.AuditMiddleware;
using WarehouseManagementSystemApi.Services.Implementations;
using WarehouseManagementSystemApi.Services.Interfaces;
using WarehouseManagementSystemApi.Validators.Employee;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();

// ---- Authentication & authorization (Identity + JWT + sessions + rate limiting) ----
builder.Services.AddIdentityServices();                              // must come before AddJwtAuthentication
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthRateLimiting(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<AuditInterceptor>();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeCreateValidator>();
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetRequiredService<AuditInterceptor>();

    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));

    options.AddInterceptors(interceptor);
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Warehouse Management System API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste only the access token (Login response -> token). Swagger adds the 'Bearer ' prefix."
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});


builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console()
        .WriteTo.File(
            "Logs/log-.txt",
            rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi().AllowAnonymous();
}


app.UseGlobelExceptionHandling();
app.CorrelationMiddleWare();
app.UseHttpsRedirection();

app.UseRateLimiter();
app.UseAuthentication();
app.RequestLogging();
app.ResponseWrapping();

app.ApiPerformanceMiddlware();
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();
