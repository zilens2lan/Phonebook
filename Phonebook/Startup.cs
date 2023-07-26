using Microsoft.EntityFrameworkCore;
using Phonebook.Config;
using Phonebook.Data;
using Phonebook.Domain.Services;
using Phonebook.Exceptions;
using Phonebook.Service;

namespace Phonebook
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IWorkerService, WorkerService>();

            var connectionString = Configuration.GetConnectionString("SqlServer");
            services.AddDbContext<DirectorsDBContext>(
                options => options.UseSqlServer(connectionString));

            services.AddMvc();
            Logger.Debug("Application connected to SQLServer", connectionString);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var loggingOptions = this.Configuration.GetSection("Log4NetCore")
                                               .Get<Log4NetProviderOptions>();
            loggerFactory.AddLog4Net(loggingOptions);

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
