using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using WarehouseManagementSystemWeb.Auth;
using WarehouseManagementSystemWeb.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // Every MVC action needs a logged-in user unless it has [AllowAnonymous] (Login, AccessDenied).
    options.Filters.Add(new AuthorizeFilter(
        new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));

    // 401 / 403 coming back from the API -> login page / access denied page.
    options.Filters.Add<ApiExceptionFilter>();
});
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddWebAuthentication(builder.Environment);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
