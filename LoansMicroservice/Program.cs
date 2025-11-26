using LoansMicroservice.Data;
using LoansMicroservice.Service;
using Microsoft.EntityFrameworkCore;
using static LoansMicroservice.Data.AppDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=loans.db"));

builder.Services.AddHttpClient();


builder.Services.AddScoped<ILoansService, LoansService>();

builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();


