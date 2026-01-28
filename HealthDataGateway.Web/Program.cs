using HealthDataGateway.Data;
using HealthDataGateway.Services;
using HealthDataGateway.Services.Implementation;
using HealthDataGateway.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<IConnectorService, ConnectorService>();
builder.Services.AddScoped<ITargetHospitalService, TargetHospitalService>();



// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();


app.MapRazorPages();

app.Run();