using ApiDotNet.Data;
using ApiDotNet.Services;
using Microsoft.EntityFrameworkCore;
using ApiDotNet.Mappings;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UsuarioProfile>();
});
builder.Services.AddControllers();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    );
});

var app = builder.Build();

app.MapControllers();

app.Run();
