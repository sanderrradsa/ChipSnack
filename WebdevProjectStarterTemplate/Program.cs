using Microsoft.AspNetCore.Authentication.Cookies;
using WebdevProjectStarterTemplate.Pages.Login;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        
        options.Cookie.Name = "Login";
        options.LoginPath = "/Login";

    });


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

Program.Configuration = app.Configuration;
//builder.services.ConfigureApplicationCookie(options => options.LoginPath = "/login");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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



app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});


app.Run();


partial class Program
{
    public static IConfiguration Configuration { get; set; } = null!;

}