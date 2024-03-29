using Demo.TaskManagement;
using Demo.TaskManagement.Areas.Identity;
using Demo.TaskManagement.Data;
using Demo.TaskManagement.Data.Entities;
using Demo.TaskManagement.Data.Triggers;
using Demo.TaskManagement.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
    .UseSqlServer(connectionString)
    .UseTriggers(triggerOptions =>
    {
        triggerOptions.AddTrigger<UserTrigger>();
    }));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserStore<CustomUserStore>();

builder.Services.AddHttpClient();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
//builder.Services.AddScoped<TasksService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id}");


var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    await RoleService.EnsureRoles(roleManager);

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await UserService.EnsureAdmin(userManager);

    var a = builder.Configuration.GetSection("Settings").Get<Settings>();
    if (a.SeedData)
    {
        await UserService.EnsureSeedDataUsers(userManager);
        await SeedData.Initialize(db, userManager);
    }
}

app.Run();
