using bukitasam.Repositories;

var builder = WebApplication.CreateBuilder(args);
// var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<BarangRepository>();
builder.Services.AddScoped<JenisRepository>();
builder.Services.AddScoped<HistoryRepository>();

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: "AllowAllOrigins",
//         policy =>
//         {
//             policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
//         });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});


app.UseAuthorization();

app.MapControllers();

app.Run();
