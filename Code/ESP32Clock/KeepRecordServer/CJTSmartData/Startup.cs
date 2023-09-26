using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TextVoiceServer.DBContext;
using TextVoiceServer.Serivices;

namespace TextVoiceServer
{
    public partial class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            _services = services;
            ////add websocket client
            //WebsocketClient wsClient = InitWebSocketClient(); 
            //services.AddSingleton<IWebsocketClient>(wsClient); 

            //config MySQL
          //  string mysqlConnectionString = ConfigurationHelper.AppSetting["DBServer:MySQLConnectionString"].ToString();
          //  services.AddDbContext<DataConfigContext>(x => x.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString)));

            //init redis 
            //InitRedis();

            services.AddControllers().AddNewtonsoftJson(
               opt =>
                 {
                     opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();//json字符串大小写原样输出
                 }) ;
            ////auto start , Microsoft.Extensions.Hosting.IHostApplicationLifetime applicationLifetime
            /// services.Configure<SensorValueCfg>(Configuration);
            //applicationLifetime.ApplicationStarted.Register(OnStartup);  

            //add cross 
            services.AddCors(); 

            services.AddHostedService<HandleSQLiteConfigService>();
           //  services.AddHostedService<HandleMQPublishService>();
          

            services.AddSingleton<HandleMQPublishService>();
            services.AddSingleton<IHostedService, HandleMQPublishService>(
                                 serviceProvider => serviceProvider.GetService<HandleMQPublishService>());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
              });
            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles(); 
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials 
        
            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthorization();

           // loggerFactory.AddLog4Net(); // << Add this line

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                // endpoints.MapHub<DataProviderHub>("/DataProvider");
            });
 


        }
    }
}
