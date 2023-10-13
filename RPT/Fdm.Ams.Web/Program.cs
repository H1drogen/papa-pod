using Fdm.Ams.ViewModels.ServiceMapperProfiles;
using Fdm.Ams.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(ServiceMapperProfile));
builder.Services.AddLogging();
builder.Services.ConfigureHttpClientForServices(builder.Configuration);
builder.Services.ConfigureAddTransientDependencyInjection();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();