using Microsoft.EntityFrameworkCore;
using MyFirstBlazorApp.Components;
using MyFirstBlazorApp.Data;
using MyFirstBlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//регистрация БД
builder.Services.AddDbContext<AppDbContext>(option => 
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<EditModeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
