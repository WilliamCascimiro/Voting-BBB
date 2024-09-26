using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    public class Load
    {
        string jsonContent = "{\"userId\": \"27B60491-1E7F-458B-9F81-88728864C0C4\", \"participantId\": \"DD2ACB5B-8F6E-42DC-9902-2AD9A22118F4\", \"paredaoId\": \"D748CE85-BA74-4922-9C58-2D346B0FC782\"}"; // Substitua pelos dados que deseja enviar

        public async Task LoadParalel(string apiUrl, int load)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(60);
                Stopwatch stopwatch = Stopwatch.StartNew();
                int sucessResponse = 0;
                int errorResponse = 0;

                try
                {
                    StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var tasks = new Task<HttpResponseMessage>[load];
                    for (var i = 0; i < tasks.Length; i++)
                    {
                        tasks[i] = client.PostAsync(apiUrl, content);
                    }

                    HttpResponseMessage[] responses = await Task.WhenAll(tasks);

                    foreach (var response in responses)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            sucessResponse++;
                        }
                        else
                        {
                            errorResponse++;
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro de requisição HTTP: {e.Message}");
                }
                finally
                {
                    stopwatch.Stop();
                    Console.WriteLine("Total de solicitações:" + (sucessResponse + errorResponse));
                    Console.WriteLine("Total sucesso:" + sucessResponse);
                    Console.WriteLine("Total erro:" + errorResponse);
                    Console.WriteLine($"Tempo total de execução: {stopwatch.Elapsed.TotalSeconds} segundos");
                }
            }
        }

        public async Task LoadSquencial(string apiUrl, int load)
        {
            using (HttpClient client = new HttpClient())
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                int sucessResponse = 0;
                int errorResponse = 0;

                client.DefaultRequestHeaders.Add("User-Agent", "Fake Agent");
                try
                {
                    StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    for (var i = 0; i < load; i++)
                    {
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            sucessResponse++;
                        }
                        else
                        {
                            errorResponse++;
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro de requisição HTTP: {e.Message}");
                }
                finally
                {
                    stopwatch.Stop();
                    Console.WriteLine("Total de solicitações:" + (sucessResponse + errorResponse));
                    Console.WriteLine("Total sucesso:" + sucessResponse);
                    Console.WriteLine("Total erro:" + errorResponse);
                    Console.WriteLine($"Tempo total de execução: {stopwatch.Elapsed.TotalSeconds} segundos");
                }
            }
        }

    }
}
