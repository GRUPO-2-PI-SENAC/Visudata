using PI.Infra.IoC;
using WebEssentials.AspNetCore.Pwa;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);
//builder.Services.AddProgressiveWebApp(new PwaOptions
//{
//    CacheId = "worker 1.1",
//    Strategy = ServiceWorkerStrategy.CacheFirst,
//    RoutesToPreCache = "Enterprise/Home , Machine/List , Enterprise/Support"
//});

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Enterprise}/{action=Login}/{id?}");

app.Run();
