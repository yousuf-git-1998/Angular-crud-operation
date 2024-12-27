using _1281533.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DeviceDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddControllersWithViews().AddNewtonsoftJson(settings =>
{
    settings.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
    settings.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
});
builder.Services.AddCors(settings =>
{
    settings.AddPolicy("corspolicy", op =>
    {
        op.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseCors("corspolicy");
app.UseRouting();
app.MapDefaultControllerRoute();


app.Run();
