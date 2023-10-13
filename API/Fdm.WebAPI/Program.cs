using Fdm.ResourcePlanningTool.Services.ServiceMapperProfiles;
using Fdm.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

string resourcePlanningToolConnectionString = "ResourcePlanningToolContext";

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(ServiceMapperProfiles));
builder.Services.ConfigureAddTransientDependencyInjection();
builder.Services.ConfigureDbContextPool(builder.Configuration, resourcePlanningToolConnectionString);
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    {
        var origins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
        policy.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.IncludeXmlComments("Fdm.WebAPI.xml"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors("CorsPolicy");

app.UseCors(builder =>
{
    builder
    .WithOrigins(new string[] { "http://localhost" })
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
});
app.UseHttpsRedirection();

app.MapControllers();

app.Run();