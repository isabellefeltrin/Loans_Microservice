using Microsoft.EntityFrameworkCore;
using LoansMicroservice.Data;
using LoansMicroservice.Repositories;
using LoansMicroservice.Services;

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
