using DemoIdentityServer.Configuration;
using DemoIdentityServer.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
var identityServerBuilder = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.AccessTokenJwtType = "JWT";
})
    // this adds the config data from DB (clients, resources)
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly("DemoIdentityServer"));
    })
    // this adds the operational data from DB (codes, tokens, consents)
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly("DemoIdentityServer"));
        // this enables automatic token cleanup. this is optional.
        options.EnableTokenCleanup = true;
    });
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseIdentityServer();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
//app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.MigrateDatabase();
app.Run();
