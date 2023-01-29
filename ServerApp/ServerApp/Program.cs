using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

var builder = WebApplication.CreateBuilder(args);
// 1. ADD DATACONTEXT 
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// ******* Add services to the container. Omit data whose values are null ***********
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. ADD CORS
builder.Services.AddCors(options => options.AddPolicy(name: "EssentialApp",
policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
// 3. SEED DATA 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.SeedDatabase(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 4. ENABLE CORS 
app.UseCors("EssentialApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
