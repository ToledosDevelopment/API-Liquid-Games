using Microsoft.Extensions.FileProviders;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsImplementationPolicy",
        builder =>
        {
            builder.WithOrigins("*"); builder.AllowAnyHeader(); builder.AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddSingleton(new MySqlConnection(builder.Configuration.GetConnectionString("MySqlConnection")));


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

app.UseHttpsRedirection();

app.UseCors("MyCorsImplementationPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();