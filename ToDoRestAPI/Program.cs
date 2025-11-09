using Microsoft.EntityFrameworkCore;
using ToDoLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

bool useDB = true;

if (useDB)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseSqlServer(connectionString));

    builder.Services.AddScoped<IRepo<ToDoLibrary.Task>, EfCoreTaskRepo>();
}
else
{
    builder.Services.AddSingleton<IRepo<ToDoLibrary.Task>, TaskRepo>();
}

    var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

}

app.UseHttpsRedirection();
    
app.UseAuthorization();

app.MapControllers();

app.Run();
