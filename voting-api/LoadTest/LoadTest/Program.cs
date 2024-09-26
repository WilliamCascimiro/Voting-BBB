using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace LoadTest
{
    internal class Program
    {
    //    static async Task Main(string[] args)
    //    {
    //        //string apiUrl = "http://localhost:5000/Paredao/Votos";
    //        string apiUrl = "http://localhost:32044/Paredao/Votos";
    //        string jsonContent = "{\"userId\": \"27B60491-1E7F-458B-9F81-88728864C0C4\", \"participantId\": \"DD2ACB5B-8F6E-42DC-9902-2AD9A22118F4\", \"paredaoId\": \"D748CE85-BA74-4922-9C58-2D346B0FC782\"}"; // Substitua pelos dados que deseja enviar

    //        using (HttpClient client = new HttpClient())
    //        {
    //            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
    //            {
    //                NoCache = true
    //            };
    //            client.DefaultRequestHeaders.Add("User-Agent", "YourUserAgent");
    //            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
    //            try
    //            {
    //                // Criação do conteúdo JSON para enviar na requisição POST
    //                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

    //                //for (var i = 0; i <= 1000; i++)
    //                //{

    //                    // Fazendo a requisição POST
    //                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

    //                    // Verificando se a resposta foi bem-sucedida
    //                    if (response.IsSuccessStatusCode)
    //                    {
    //                        string result = await response.Content.ReadAsStringAsync();
    //                        Console.WriteLine("Resposta da API:");
    //                        Console.WriteLine(result);
    //                    }
    //                    else
    //                    {
    //                        Console.WriteLine($"Erro ao acessar a API: {response.StatusCode}");
    //                    }
    //               // }
    //            }
    //            catch (IOException ex)
    //            {
    //                Console.WriteLine($"IOException: {ex.Message}");
    //                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
    //            }
    //            catch (HttpRequestException e)
    //            {
    //                Console.WriteLine($"Erro de requisição HTTP: {e.Message}");
    //            }
    //        }
    //    }

        static async Task Main(string[] args)
        {
        //string apiUrl = "http://localhost:5000/Paredao/Votos";
        string apiUrl = "http://localhost:32044/Paredao/Votos";
        string jsonContent = "{\"userId\": \"27B60491-1E7F-458B-9F81-88728864C0C4\", \"participantId\": \"DD2ACB5B-8F6E-42DC-9902-2AD9A22118F4\", \"paredaoId\": \"D748CE85-BA74-4922-9C58-2D346B0FC782\"}"; // Substitua pelos dados que deseja enviar

        using (HttpClient client = new HttpClient())
        {
            client.Timeout = TimeSpan.FromSeconds(20); // Definindo o tempo limite de 10 segundos
            Stopwatch stopwatch = Stopwatch.StartNew();
            int sucessResponse = 0;
            int errorResponse = 0;

            try
            {
                // Criação do conteúdo JSON para enviar na requisição POST
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var tasks = new Task<HttpResponseMessage>[1000];
                for (var i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = client.PostAsync(apiUrl, content);
                }

                HttpResponseMessage[] responses = await Task.WhenAll(tasks);

                foreach (var response in responses)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //string result = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine("Resposta da API:");
                        //Console.WriteLine(result);
                        sucessResponse++;
                    }
                    else
                    {
                        errorResponse++;
                        //Console.WriteLine($"Erro ao acessar a API: {response.StatusCode}");
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
