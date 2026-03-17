using HRManagement.Web.Services;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add session for storing JWT token
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register HttpClient and API services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TokenHandler>();
builder.Services.AddHttpClient("HRApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7001/"); // Your API URL
}).AddHttpMessageHandler<TokenHandler>();

builder.Services.AddScoped<AuthApiService>();
builder.Services.AddScoped<EmployeeApiService>();
builder.Services.AddScoped<AttendanceApiService>();
builder.Services.AddScoped<PayrollApiService>();
builder.Services.AddScoped<OrganizationApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
