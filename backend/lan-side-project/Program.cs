﻿
using lan_side_project.Data;
using lan_side_project.Middlewares;
using lan_side_project.Repositories;
using lan_side_project.Services;
using lan_side_project.Utils;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace lan_side_project;

public class Program
{
    public static void Main(string[] args)
    {
        // 如果需要看 serilog 的 log 在開啟
        //Serilog.Debugging.SelfLog.Enable(Console.Out);
        try
        {
            Log.Information("Starting web host");
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            // 從 appsettings.json 讀取 Serilog 設定
            builder.Host.UseSerilog((context, services, configuration) =>
                 configuration.ReadFrom.Configuration(context.Configuration));

            // 配置 EF Core 與 PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 註冊 Repository
            builder.Services.AddScoped<UserRepository>();

            // 註冊 Service
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<UserService>();

            // 註冊 Utils
            builder.Services.AddSingleton<JwtUtils>();



            // Add services to the container.
            //builder.Services.AddAuthorization();

            // 設定 Google OAuth 2.0 認證
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "Cookies"; // 使用 Cookie 儲存用戶會話
            //    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // 設定 Google 作為挑戰方案
            //})
            //.AddGoogle(options =>
            //{
            //    options.ClientId = builder.Configuration["GOOGLE_CLIENT_ID"] ?? "";
            //    options.ClientSecret = builder.Configuration["GOOGLE_SECRET_KEY"] ?? "";
            //    options.Scope.Add("email"); // 請求 email 權限
            //    options.SaveTokens = true; // 儲存 token，這樣可以用來生成 JWT
            //});

            // 配置 JWT 認證
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // 當驗證失敗時，回應標頭會包含 WWW-Authenticate 標頭，這裡會顯示失敗的詳細錯誤原因
                    options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                        // 一般我們都會驗證 Issuer
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),

                        // 通常不太需要驗證 Audience
                        ValidateAudience = false,

                        // 一般我們都會驗證 Token 的有效期間
                        ValidateLifetime = true,

                        // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                        ValidateIssuerSigningKey = true,

                        // "1234567890123456" 應該從 IConfiguration 取得
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSettings:SecretKey"))),

                        //沒有設定的話預設為5分鐘，這會導致過期時間會再增加
                        ClockSkew = TimeSpan.Zero
                    };
                });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // 註冊控制器並修改模型驗證失敗時的行為
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = context.ModelState
                            .Where(m => m.Value?.Errors.Count > 0)
                            .Select(m => new
                            {
                                Field = m.Key,
                                Messages = m.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                            });

                        var errorResponse = new
                        {
                            Code = "ValidationError",
                            Message = "Input validation failed.",
                            Errors = errors
                        };

                        return new BadRequestObjectResult(errorResponse);
                    };
                });
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            try
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
                Console.WriteLine("Database migration applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during migration: {ex.Message}");
            }


            app.UseSerilogHttpSessionsLogging(HttpSessionInfoToLog.All);

            // 註冊 ExceptionHandlingMiddleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            // 啟用身份驗證和授權
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
