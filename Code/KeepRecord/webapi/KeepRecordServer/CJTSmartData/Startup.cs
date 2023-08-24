using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            string mysqlConnectionString = ConfigurationHelper.AppSetting["DBServer:MySQLConnectionString"].ToString();
            services.AddDbContext<DataConfigContext>(x => x.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString)));

            //init redis 
            //InitRedis();

            services.AddControllers().AddNewtonsoftJson();
            ////auto start , Microsoft.Extensions.Hosting.IHostApplicationLifetime applicationLifetime
            /// services.Configure<SensorValueCfg>(Configuration);
            //applicationLifetime.ApplicationStarted.Register(OnStartup);  

            //add cross 
            services.AddCors();
            services.AddControllers();  

            //services.AddHostedService<HandleSystemCfgService>();
            //services.AddHostedService<HandleMQPublishService>();
            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            //// global cors policy
            //app.UseCors(x => x
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .SetIsOriginAllowed(origin => true) // allow any origin
            //    .AllowCredentials()); // allow credentials

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                // endpoints.MapHub<DataProviderHub>("/DataProvider");
            });

        }
    }
}
