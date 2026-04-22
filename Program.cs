using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AeonRegistryAPI.Endpoints.Home;
using AeonRegistryAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddCustomSwagger();

//get a connection to the database
var connectionString = DataUtility.GetConnectionString(builder.Configuration);

//Configure the database context for PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

//Add identity endpoints
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//Admin policy
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

//Enable validation for minimal APIs
builder.Services.AddValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

var authRoutGroup = app.MapGroup("api/auth").WithTags("Admin");
authRoutGroup.MapIdentityApi<ApplicationUser>();
app.MapHomeEndpoints();

app.Run();