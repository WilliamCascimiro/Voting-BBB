using ConsoleApp1;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace LoadTest
{
    internal class Program
    {


        static async Task Main(string[] args)
        {
            var load = new Load();

            //await Load("https://localhost:44349/Paredao/Votos-BD-Async", 1);
            //await Load("https://localhost:44349/Paredao/Votos-BD-Sync", 1);
            //await Load("https://localhost:44349/Paredao/Votos-BD-Async", 1);
            //await Load("https://localhost:44349/Paredao/Votos-BD-Async", 1);

            //await Load("https://localhost:44349/Paredao/Votos-BD-Async", 100);
            //await Load("https://localhost:44349/Paredao/Votos-BD-Sync", 100);
            //await Load("https://localhost:44349/Paredao/Votos-Queue-Async", 100);
            //await Load("https://localhost:44349/Paredao/Votos-Queue-Sync", 100);

            //await Load("https://localhost:44349/Paredao/Votos-BD-Async", 1000);
            //await Load("https://localhost:44349/Paredao/Votos-BD-Sync", 1000);
            //await Load("https://localhost:44349/Paredao/Votos-Queue-Async", 1000);
            //await Load("https://localhost:44349/Paredao/Votos-Queue-Sync", 1000);

            //await Load("https://localhost:44349/Paredao/Votos-BD-Async", 10000);
            //await Load("https://localhost:44349/Paredao/Votos-BD-Sync", 10000);
            //await Load("https://localhost:44349/Paredao/Votos-Queue-Sync", 10000);
            //await load.LoadParalel("http://localhost:80/Paredao/Votos-Queue-Async", 10000);
            await load.LoadSquencial("http://localhost:80/Paredao/Votos-Queue-Async", 3);



        }
        

    }
}
