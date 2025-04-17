using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Repositories;
using PuzzleLab.Infrastructure.Persistence;
using PuzzleLab.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DatabaseContext>(options => {
    options.UseNpgsql(connectionString,
        npgsqlOptions => {
            npgsqlOptions.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName);
        });
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssembly(typeof
        (PuzzleLab.Application.Features.Auth.Commands.LoginCommand).Assembly));

builder.Services.AddControllers();

var app = builder.Build();
var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
db.Database.Migrate();

app.MapControllers();
app.Run();