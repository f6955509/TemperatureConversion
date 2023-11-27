

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Temperature.Conversion.Services.Middleware;

namespace Temperature.Conversion.Services
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "Cors_Policy",
                                  builder =>
                                  {
                                      builder
                                        .WithOrigins("http://localhost:4200", "http://localhost:61239") // specifying the allowed origin
                                        .WithMethods("POST","GET") // defining the allowed HTTP method
                                        .AllowAnyHeader(); // allowing any header to be sent
                                  });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });

            app.UseCors("Cors_Policy");

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.UseMiddleware<AuditLogMiddleware>();
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });
            app.Run();
        }
        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

