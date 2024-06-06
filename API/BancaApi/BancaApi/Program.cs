using BancaApi.Repository.IRepository;
using BancaApi.Repository.Repository;
using BancaApi.Services.IService;
using BancaApi.Services.Service;
using BancaApi.Util;
using Ganss.Xss;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<SqlContext>();
builder.Services.AddScoped<HtmlSanitizer>();

// Register services
builder.Services.AddScoped<ICuentaBancariaService, CuentaBancariaService>();
builder.Services.AddScoped<ITransaccionService, TransaccionService>();

// Register repositories
builder.Services.AddScoped<ICuentaBancariaRepository, CuentaBancariaRepository>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();

// Register IHttpClientFactory
builder.Services.AddHttpClient();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
