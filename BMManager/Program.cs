using BMManager.BMManagerUI;
using Microsoft.EntityFrameworkCore;
using BMManager.BMManagerCD;
using BMManagerLN;
using BMManager;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<BMManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(Constants.AuthScheme)
    .AddCookie(Constants.AuthScheme, options =>
    {
        options.Cookie.Name = Constants.AuthCookie;
        options.LoginPath = "/";
        options.AccessDeniedPath = "/acessonegado";
        options.LogoutPath = "/terminarsessao";

        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;

    });


builder.Services.AddSingleton<MovelTemp>();


builder.Services.AddScoped<APIBMManagerLN, BMManagerLN.BMManagerLN>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication().
    UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
