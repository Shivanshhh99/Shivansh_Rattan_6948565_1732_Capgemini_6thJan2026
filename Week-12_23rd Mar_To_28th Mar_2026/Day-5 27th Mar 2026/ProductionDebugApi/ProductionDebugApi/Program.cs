using ProductionDebuggingApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Log4Net
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddLog4Net("log4net.config");

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use Performance Logging Middleware
app.UseMiddleware<PerformanceLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();