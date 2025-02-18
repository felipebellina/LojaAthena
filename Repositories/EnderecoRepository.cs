using System.Net.Http.Headers;
using LojaAthena.Repositories.Interfaces;
using LojaAthena.Models;

namespace LojaAthena.Repositories;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly HttpClient _httpClient;
    public EnderecoRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://viacep.com.br");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

   /* public static string Method(string path)
    {
        using (var client = new HttpClient())
        {
            var response = client.GetAsync(path).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                return responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
            }
        }
    }*/

    public EnderecoModel GetEndereco(string cep)
    {

        try
        {
            var response = _httpClient.GetAsync($"/ws/{cep}/json").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var endereco = response.Content.ReadAsAsync<EnderecoModel>().Result;

                return endereco;
            }
            else
            {
                return null;
            }

        }
        catch (Exception messagem)
        {

            throw;
        }

    }


    /*public  EnderecoModel GetEndereco(string cep)
    {

        try
        {
            var response = await _httpClient.GetAsync($"/ws/{cep}/json");

            if (response.IsSuccessStatusCode)
            {
                var endereco = await response.Content.ReadAsAsync<EnderecoModel>();
                return endereco;
            }
            else {
                return null;
            }

        }
        catch (Exception messagem)
        {

            throw; 
        }

    }*/
}
