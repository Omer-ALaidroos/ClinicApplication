using ClinicApp.Application.DependencyInjection;
using ClinicApp.Host.Middleware;
using ClinicApp.Infrastucture.DependencyInjection;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog to read from appsettings
Log.Logger = new LoggerConfiguration()
   // .MinimumLevel.Error()
    //.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        rollOnFileSizeLimit: true
    )
    .CreateLogger();

builder.Host.UseSerilog();

Log.Logger.Information("Application is Building ...");

// Add services to the container.
builder.Services.AddControllers();
builder.Configuration.AddJsonFile("AppConfig.json");
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationService();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
