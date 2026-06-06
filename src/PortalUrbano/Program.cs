using Microsoft.EntityFrameworkCore;
using portal_urbano.Data;
using portal_urbano.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configuração de porta dinâmica para hospedagem (Render)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// Configuração padrão para Razor Pages e MVC
builder.Services.AddControllersWithViews();

// Configurar SendGrid / serviço de email
builder.Services.Configure<portal_urbano.Services.Email.SendGridOptions>(builder.Configuration.GetSection("SendGrid"));
builder.Services.AddHttpClient("SendGridHealth");
builder.Services.AddSingleton<portal_urbano.Services.Email.IEmailService, portal_urbano.Services.Email.SendGridEmailService>();
builder.Services.AddHostedService<portal_urbano.Services.Email.SendGridValidationHostedService>();

// Autenticação por cookie simples
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion));

// Registrar serviço de moderação (usa HttpClient)
builder.Services.AddHttpClient<portal_urbano.Services.GeminiModeracaoService>();

// Configurar Supabase
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:Key"];
var optionsSupabase = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = false
};
var supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey, optionsSupabase);
// Inicializa sincronamente para garantir que a instancia esteja pronta ao adicionar como singleton.
supabaseClient.InitializeAsync().GetAwaiter().GetResult();
builder.Services.AddSingleton(supabaseClient);

var app = builder.Build();

// Middleware para responder 200 OK em requisições HEAD no root (evita erro 405 no health check do Render)
app.Use(async (context, next) =>
{
    if (context.Request.Method == "HEAD" && context.Request.Path == "/")
    {
        context.Response.StatusCode = 200;
        return;
    }
    await next();
});

// Aplicar as migrações automáticas no banco de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações automáticas no banco de dados.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
