using OsztottSzavazasiRendszerBE;
using Microsoft.EntityFrameworkCore;
using OsztottSzavazasiRendszerBE.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var connectionString = StartupHelper.GetConnectionString(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
