using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApplication1.Models;

/*  The Program. cs file is where: Services required by the app are
    configured. The app's request handling pipeline is defined as a
    series of middleware components.
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>


(options => options.UseSqlite(builder.Configuration
.GetConnectionString("WebApiDatabase")));
//**********************************



builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = "Application";
    o.DefaultSignInScheme = "External";
})
            .AddCookie("Application")
            .AddCookie("External")
            .AddGoogle(o =>
            {
                //Google Client id
                o.ClientId = "25178160687-qrevgu2uf3a5egco6bsl4f0vfpscv3jo.apps.googleusercontent.com";
                //Google Client Secret Key
                o.ClientSecret = "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";
                //Return Url
                o.ReturnUrlParameter = "https://localhost:7244/";
                o.Scope.Add("profile");
                o.Events.OnCreatingTicket = (context) =>
                {
                    var picture = context.User.GetProperty("picture").GetString();

                    context.Identity.AddClaim(new Claim("picture", picture));

                    return Task.CompletedTask;
                };
            });

//************************************

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


app.UseAuthentication();
app.UseAuthorization();

//Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
