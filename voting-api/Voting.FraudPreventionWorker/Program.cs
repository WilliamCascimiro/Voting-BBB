using Voting.FraudPreventionWorker.Application;

namespace Voting.FraudPreventionWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            builder.Services.AddSingleton<Producer>();
            builder.Services.AddSingleton<Consumer>();

            var host = builder.Build();
            host.Run();
        }
    }
}