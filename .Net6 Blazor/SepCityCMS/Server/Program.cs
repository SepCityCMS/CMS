using SepCityCMS.Server.Security.Bearer.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace SepCityCMS.Server
{
    public class Program
    {
        public IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            
            //builder.Configuration.AddJsonFile("jwt.json", true, true);
            //builder.Configuration.AddJsonFile("license.json", true, true);
            //builder.Configuration.AddJsonFile("points.json", true, true);
            //builder.Configuration.AddJsonFile("security.json", true, true);
            //builder.Configuration.AddJsonFile("settings.json", true, true);
            //builder.Configuration.AddJsonFile("system.json", true, true);

            var cfgJsonFile = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("jwt.json", true, true).Build();
            builder.Services.AddOptions<SepCityCMS.Models.Config.Jwt.Root>()
            .Bind(cfgJsonFile)
            .ValidateDataAnnotations();

            cfgJsonFile = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("license.json", true, true).Build();
            builder.Services.AddOptions<SepCityCMS.Models.Config.License.Root>()
            .Bind(cfgJsonFile)
            .ValidateDataAnnotations();

            cfgJsonFile = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("points.json", true, true).Build();
            builder.Services.AddOptions<SepCityCMS.Models.Config.Points.Root>()
            .Bind(cfgJsonFile)
            .ValidateDataAnnotations();

            cfgJsonFile = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("security.json", true, true).Build();
            builder.Services.AddOptions<SepCityCMS.Models.Config.Security.Root>()
            .Bind(cfgJsonFile)
            .ValidateDataAnnotations();

            cfgJsonFile = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("settings.json", true, true).Build();
            builder.Services.AddOptions<SepCityCMS.Models.Config.Settings.Root>()
            .Bind(cfgJsonFile)
            .ValidateDataAnnotations();

            cfgJsonFile = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("system.json", true, true).Build();
            builder.Services.AddOptions<SepCityCMS.Models.Config.System.Root>()
            .Bind(cfgJsonFile)
            .ValidateDataAnnotations();

            //var cfgJwt = new Config.ConfigJwt.Root();
            //ConfigurationBinder.Bind(cfgJwtFile, cfgJwt);
            //builder.Services.AddSingleton(cfgJwt);

            //var enKey = cfgJwt.Jwt.Key;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "SepCityCMS.Server.Security.Bearer",
                    ValidAudience = "SepCityCMS.Server.Security.Bearer",
                    IssuerSigningKey = JwtSecurityKey.Create("fiver-secret-key")
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Token == null)
                        {
                            context.Token = context.HttpContext.Request.Headers["Authorization"];
                            if (context.Token != null)
                            {
                                var tokenHandler = new JwtSecurityTokenHandler();
                                var key = Encoding.ASCII.GetBytes("fiver-secret-key");
                                tokenHandler.ValidateToken(context.Token.Replace("Bearer ", "").Trim(), new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = false,
                                    IssuerSigningKey = new SymmetricSecurityKey(key),
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ClockSkew = TimeSpan.FromHours(20)
                                }, out SecurityToken validatedToken);

                                var jwtToken = (JwtSecurityToken)validatedToken;
                                var accountId = jwtToken.Claims.First(x => x.Type == "UserId").Value;
                            }
                        }
                        Console.WriteLine("OnMessageReceived: " + context.Token);
                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        Console.WriteLine("OnForbidden: " + context.Response);
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine("OnChallenge: " + context.Response);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }

    }
}

