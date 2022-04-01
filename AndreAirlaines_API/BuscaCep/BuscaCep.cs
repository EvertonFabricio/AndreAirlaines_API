using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AndreAirlaines_API.BuscaCep
{
    public class BuscaCep
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main()
        {
            Console.Write("CEP: ");
            var cep = Console.ReadLine();
            try
            {

                HttpResponseMessage response = await client.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
               
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }


        }
    }

}

