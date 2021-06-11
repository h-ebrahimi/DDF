using DDF.Common.GeneralSettings;
using DDF.Services.Contract.Infrastructure;
using DDF.Services.Contract.Persistence;
using DDF.Services.Contract.Usecases;
using DDF.Services.Default.Infrastructure;
using DDF.Services.Default.Persistence;
using DDF.Services.Default.Usecases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDF
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddOptions();
            services.Configure<DDFSettings>(Configuration.GetSection("DDFSettings"));
            services.Configure<MongodbSettings>(Configuration.GetSection("MongodbSettings"));

            services.AddScoped<IMetadataService, MetadataService>();
            services.AddScoped<IMetadataTransactionService, MetadataTransactionService>();
            services.AddSingleton<ILoginTransactionService, LoginTransactionService>();
            services.AddScoped<IMetadataTransactionUsecaseService, MetadataTransactionUsecaseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
