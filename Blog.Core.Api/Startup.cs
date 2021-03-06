using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Blog.Core.Api.Filters;
using Blog.Core.Api.Log;
using Blog.Core.Api.SwaggerHelper;
using Blog.Core.Common.DB;
using Blog.Core.Common.Helper;
using Blog.Core.Extensions.AutoMapper;
using Blog.Core.Extensions.Service;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Blog.Core.Api
{
    public class Startup
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ApiName { get; set; } = "Blog.Core";

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 日志仓库
        /// </summary>
        /// <value></value>
        public static ILoggerRepository repository { get; set; }

        public Startup(IConfiguration configuration)
        {
            // 配置
            Configuration = configuration;

            // log4net 日志
            repository = LogManager.CreateRepository("Blog.Core.Api");   // 需要获取日志的仓库名

            // 指定配置文件,如果这里你遇到问题,应该是使用了 InProcess 模式,请查看项目文件并删除该配置
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));  // 配置文件
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 注入全局异常捕获
            services.AddMvc(s => {
                s.Filters.Add(typeof(GlobalExceptionFilter));
            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            // 注入 Configuration
            services.AddSingleton(new Appsettings(Configuration));

            // 注入 Log4Net 日志
            services.AddSingleton<ILoggerHelper, LogHelper>();

            // 配置跨域请求策略
            services.AddCors(c =>
            {
                c.AddPolicy("CorsIpAccess", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // 配置Swagger
            services.AddSwaggerGen(c => {
                typeof(CustomApiVersion).GetEnumNames().ToList().ForEach(version => {
                    c.SwaggerDoc(version, new OpenApiInfo(){
                        Title = $"{ApiName}接口文档——NetCore 3.0",
                        Description = $"{ApiName} HTTP API {version}",
                        Contact = new OpenApiContact(){ Name = ApiName, Email = "1044457987@qq.com", Url = new Uri("https://www.jianshu.com/u/00b7351bacae")},
                        License = new OpenApiLicense(){ Name = ApiName, Url = new Uri("https://www.jianshu.com/u/00b7351bacae")}
                    });
                });

                c.OrderActionsBy(o => o.RelativePath);

                // 配置注释文件
                string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                try
                {
                    // Api注释路径
                    string apiXmlPath = Path.Combine(basePath, "Blog.Core.Api.xml");
                    c.IncludeXmlComments(apiXmlPath, true);
                    // Model注释路径
                    string modelXmlPath = Path.Combine(basePath, "Blog.Core.Model.xml");
                    c.IncludeXmlComments(modelXmlPath, true);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

                // 开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在Header中添加Token,传递到后端
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                #region 将Token绑定到ConfigureService
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme(){
                        Description = "JWT授权（数据将在请求头中进行传输）直接在下框中输入\"Bearer {token}\"（二者间用空格分隔）",
                        Name = "Authorization",             // Jwt默认的参数名称
                        In = ParameterLocation.Header,      // Jwt默认存放Authorization信息的位置（请求头）
                        Type = SecuritySchemeType.ApiKey
                    });
                #endregion
            });
            
            #region 创建自定义授权策略
                services.AddAuthorization(options => {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            });
            #endregion

            #region 官方默认认证
                var audienceConfig = Configuration.GetSection("Audience");
                var symmtricKeyAsBase64 = AppSecretConfig.Audience_Secret_String;
                var keyByteArray = Encoding.ASCII.GetBytes(symmtricKeyAsBase64);
                var signingKey = new SymmetricSecurityKey(keyByteArray);
                var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = audienceConfig["Issuer"],     // 发行人
                    ValidateAudience = true,
                    ValidAudience = audienceConfig["Audience"], // 订阅人
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30),
                    RequireExpirationTime = true
                };

                // 开启Bearer认证
                services.AddAuthentication("Bearer")
                    // 添加JwtBearer服务
                    .AddJwtBearer(o => 
                    {
                        o.TokenValidationParameters = tokenValidationParameters;
                        o.Events = new JwtBearerEvents()
                        {
                            OnAuthenticationFailed = context =>
                            {
                                // 如果过期,则把<是否过期>添加到返回头信息中
                                if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                {
                                    context.Response.Headers.Add("Token-Expired", "true");
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });
            #endregion

            services.AddControllers();

            // AutoMapper实体映射
            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // 使用静态文件
            app.UseStaticFiles();

            // 使用Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                typeof(CustomApiVersion).GetEnumNames().OrderByDescending(versionName => versionName).ToList().ForEach(version => {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json",$"{ApiName} {version}");
                });
                //路径配置
                c.RoutePrefix = "apidoc"; 
            });

            app.UseRouting();

            // 跨域请求处理中间件
            app.UseCors("CorsIpAccess");

            // 自定义鉴权中间件
            // app.UseJwtTokenAuth();

            // 官方鉴权中间件
            app.UseAuthentication();
            // 授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// 配置Autofac容器，在Program.cs的CreateHostBuilder中添加Autofac服务工厂
        /// </summary>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }
    }
}
