using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json.Serialization;
using TareasMVC;
using TareasMVC.Servicios;

var builder = WebApplication.CreateBuilder(args);

var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
}).AddJsonOptions(opciones =>
{
    opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conectionString));

builder.Services.AddAuthentication();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/usuarios/login";
    });

builder.Services.AddLocalization(opciones =>
{
    opciones.ResourcesPath = "Recursos";
});

builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

var culturasUISoportadas = new[] { "es", "en" };

app.UseRequestLocalization(opciones =>
{
    opciones.DefaultRequestCulture = new RequestCulture("es");
    opciones.SupportedCultures = culturasUISoportadas.Select(cultura => new CultureInfo(cultura)).ToList();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
