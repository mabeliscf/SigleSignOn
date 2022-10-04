
using Microsoft.EntityFrameworkCore;
using QRA.Persistence;
using QRA.UseCases.Queries;

namespace QRA.API
{

    public class Startup
    {

        public readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

      

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers(options => options.Filters.Add<ValidationResultAttribute>())
            //    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BillingFiltersValidator>());


            string QRAConnectionString = Configuration.GetConnectionString(name: "QRAChallenge");
            int timeout = Configuration.GetValue<int>("Timeout");

            services.AddDbContext<QRAchallengeContext>(options =>
                options.UseSqlServer(QRAConnectionString, sql => sql.CommandTimeout(timeout)).UseLazyLoadingProxies());

            //cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowCredentials().WithOrigins("http://localhost:4200");
                                     

                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();

                                  });
            });

#if Develop
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.Api", Version = "v1" });
            });
#endif

            // services.AddAutoMapper(typeof(BillingProfile).Assembly);

            //services.ConfigDataBaseServices(Configuration);

            //services.ConfigIoCServices(Configuration);

            //services.ConfigIoCForFactories();

            //services.ConfigIoCForCommands();

            //services.ConfigIoCForQueries();

            services.AddControllers();
            //add queries 
            services.AddScoped<GetTenantQueries>();


            services.AddHealthChecks();

            services.AddLogging();
            services.AddMvc();
        }

        /// <summary>
        /// Este método se llama el tiempo de ejecución. Utilice este método para configurar la tubería de solicitud HTTP.
        /// </summary>
        /// <param name="app">Define una clase que proporciona los mecanismos para configurar la tubería de solicitud de una aplicación.</param>
        /// <param name="env">Proporciona información sobre el entorno de alojamiento web en la que se ejecuta una aplicación.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseSwagger();

                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library.Api v1");
                //    c.RoutePrefix = string.Empty;
                //});
            }else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseCors(MyAllowSpecificOrigins);

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
