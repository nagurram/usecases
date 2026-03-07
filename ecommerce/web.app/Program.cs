using web.app.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddApiDocumentation();
builder.Services.AddApplicationServices();
builder.Services.AddCorsConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseApiDocumentation(app.Environment);
app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();

// BFF: Serve React app from wwwroot with SPA routing
app.UseBFFStaticFiles();

// Map API and health endpoints
app.MapApplicationEndpoints();

app.Run();
