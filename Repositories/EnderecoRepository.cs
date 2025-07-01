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
    public EnderecoModel GetEndereco(string cep)
    {

        try
        {
            var response = _httpClient.GetAsync($"/ws/{cep}/json").GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var endereco = response.Content.ReadFromJsonAsync<EnderecoModel>().Result;

                return endereco;
            }
            else
            {
                return null;
            }

        }
        catch (Exception menssagem)
        {

            throw;
        }

    }

}
