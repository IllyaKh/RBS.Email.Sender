using Microsoft.OpenApi.Models;
using RBS.Email.Sender.Common.Configuration;
using RBS.Email.Sender.DataAccess.DataAccess;
using RBS.Email.Sender.DataAccess.Interfaces;
using RBS.Email.Sender.Services;
using RBS.Email.Sender.Services.Interface;

namespace RBS.Email.Sender.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

            if (Environment.IsProduction())
            {
                var bytes = Convert.FromBase64String(System.Environment.GetEnvironmentVariable("AppSettings"));
                builder.AddJsonStream(new MemoryStream(bytes));
            }
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(Startup));

            ConfigureInternalServices(services);

            services.AddControllers();

            services.Configure<EmailOptions>(Configuration.GetSection("EmailSettings"));
            services.Configure<MongoDbOptions>(Configuration.GetSection("MongoSettings"));


            if (!Environment.IsProduction())
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "RBS.Email.Sender", Version = "v1" });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    opt.RoutePrefix = string.Empty;
                });
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ConfigureInternalServices(IServiceCollection services)
        {
            services.AddScoped<ISenderService, SenderService>();
            services.AddScoped<IEmailDataService, EmailDataService>();
            services.AddScoped<ISendEmailItemDataAccess, SendEmailItemDataAccess>();
        }
    }

}
