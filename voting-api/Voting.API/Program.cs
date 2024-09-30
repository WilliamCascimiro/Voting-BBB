using Serilog;
using Serilog.Events;
using Voting.API.Middlewares;
using Voting.Infra.IoC;

namespace Voting.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Host.UseSerilog((ctx, lc) => lc
            //    .WriteTo.Console(LogEventLevel.Debug)
            //    .WriteTo.File("log.txt",
            //        LogEventLevel.Warning,
            //    rollingInterval: RollingInterval.Day));


            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(2);
            });

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
                //options.InstanceName = "VotingApp_";
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks();

            builder.Services.RegisterServices(builder.Configuration);

            var port = builder.Configuration["PORT"];
            builder.WebHost.UseUrls($"http://*:{port}");

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseHealthChecks("/health");

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
