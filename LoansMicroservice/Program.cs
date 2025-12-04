<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using LoansMicroservice.Data;
using LoansMicroservice.Repositories;
using LoansMicroservice.Services;
=======
using LoansMicroservice.Data;
using LoansMicroservice.Service;
using Microsoft.EntityFrameworkCore;
using static LoansMicroservice.Data.AppDbContext;
>>>>>>> 54da52fad984003a64833e166d416e5bbcf56549

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LoansDbContext>(options =>
    options.UseSqlite("Data Source=loans.db")
);

builder.Services.AddHttpClient<ExternalServicesHelper>();
builder.Services.AddScoped<LoansRepository>();
builder.Services.AddScoped<LoansService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

<<<<<<< HEAD
=======

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=loans.db"));

builder.Services.AddHttpClient();


builder.Services.AddScoped<ILoansService, LoansService>();

builder.Services.AddControllers();


>>>>>>> 54da52fad984003a64833e166d416e5bbcf56549
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LoansDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Urls.Add("http://localhost:5090");
app.Run();


