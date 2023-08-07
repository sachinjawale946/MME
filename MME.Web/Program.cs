using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MME.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MME.Web.JWT;
using Microsoft.AspNetCore.Mvc.Filters;
using MME.Web.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options => options.Cookie.HttpOnly = true);
builder.Services.AddSession(options => options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest);
builder.Services.AddSession(options => options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict);

var defaultApp = FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my-community.json")),
});

// Authentication with JWT
builder.Services.AddAuthentication(x =>
{
    //x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = "MultiAuthSchemes";
    x.DefaultChallengeScheme = "MultiAuthSchemes";
})
    .AddCookie(options =>
{
    options.LoginPath = "/Account/Unauthorized/";
    options.AccessDeniedPath = "/Account/Forbidden/";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
})
       .AddJwtBearer(o =>
       {
           var Key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Key"));
           o.SaveToken = true;
           o.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = false,
               ValidateAudience = false,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer"),
               ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience"),
               IssuerSigningKey = new SymmetricSecurityKey(Key)
           };
       })
    .AddJwtBearer("MMEJwtScheme", o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:Key"));
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("JWT:Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
}).AddPolicyScheme("MultiAuthSchemes", JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.ForwardDefaultSelector = context =>
    {
        string authorization = context.Request.Headers[HeaderNames.Authorization];
        if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
        {
            var token = authorization.Substring("Bearer ".Length).Trim();
            var jwtHandler = new JwtSecurityTokenHandler();
            return (jwtHandler.CanReadToken(token) && jwtHandler.ReadJwtToken(token).Issuer.Equals(builder.Configuration.GetValue<string>("JWT:Issuer")))
                ? JwtBearerDefaults.AuthenticationScheme : "MMEJwtScheme";
        }
        return CookieAuthenticationDefaults.AuthenticationScheme;
    };
});

builder.Services.AddAuthorization(options =>
{
    var onlySecondJwtSchemePolicyBuilder = new AuthorizationPolicyBuilder("MMEJwtScheme");
    options.AddPolicy("MMEJwtScheme", onlySecondJwtSchemePolicyBuilder
        .RequireAuthenticatedUser()
        .Build());
    var onlyCookieSchemePolicyBuilder = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme);
    options.AddPolicy("MMECookieScheme", onlyCookieSchemePolicyBuilder
        .RequireAuthenticatedUser()
        .Build());
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MMEAppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MMEAppDBConnection")));

builder.Services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddScoped<MMEExceptionFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
