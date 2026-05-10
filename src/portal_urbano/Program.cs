using Microsoft.EntityFrameworkCore;
using ProjetoUrbano.Data;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseStaticWebAssets();
builder.WebHost.UseUrls("http://localhost:5006", "https://localhost:7119");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

// Serviço de e-mail (SendGrid)
builder.Services.Configure<ProjetoUrbano.Services.Email.SendGridOptions>(
    builder.Configuration.GetSection("SendGrid"));
builder.Services.AddScoped<ProjetoUrbano.Services.Email.IEmailService,
    ProjetoUrbano.Services.Email.SendGridEmailService>();

// Banco de dados MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Supabase (upload de imagens)
var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseKey = builder.Configuration["Supabase:Key"];
var supabaseOptions = new Supabase.SupabaseOptions { AutoConnectRealtime = false };
var supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey, supabaseOptions);
supabaseClient.InitializeAsync().Wait();
builder.Services.AddSingleton(supabaseClient);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}")
    .WithStaticAssets();

app.MapControllers();

app.Run();
