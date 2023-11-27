using Microsoft.AspNetCore.HttpOverrides;
using Temperature.Conversion.Services.Middleware;

namespace Temperature.Conversion.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Cors_Policy",
                    builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app,
            IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseMiddleware<AuditLogMiddleware>();

            app.UseCors("Cors_Policy");
            app.UseRouting();
        }

    }
}
