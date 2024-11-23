using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Trabajo_Final.Data;
using Trabajo_Final.Models; // Asegúrate de que esto esté aquí para acceder a ExternalApiService

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuración de Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Configuración de MVC
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<Trabajo_Final.Integration.CurrencyExchange.CurrencyExchangeIntegration>();
builder.Services.AddScoped<Trabajo_Final.Services.ProductoService>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1500);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configuración de HttpClient para ExternalApiService con URL base desde appsettings.json
builder.Services.AddHttpClient<ExternalApiService>(client =>
{
    var apiBaseUrl = builder.Configuration["ExternalApi:BaseUrl"]; // Cambiado a ExternalApi
    if (string.IsNullOrEmpty(apiBaseUrl))
    {
        throw new InvalidOperationException("Base URL for external API is not configured.");
    }
    client.BaseAddress = new Uri(apiBaseUrl);
});

// Configuración de Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1",
        Description = "API DE PRODUCTOS"
    });
});
var app = builder.Build();

// Configuración de pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();


app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
