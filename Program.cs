

using GamingWebsite.Servics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionString") ??
                        throw new InvalidOperationException("No connection String was found");

builder.Services.AddDbContext<GamingWebsite.Data.AppContext>(options =>
    options.UseSqlServer(ConnectionString));

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();
builder.Services.AddScoped<IGameService,GameService>();
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
