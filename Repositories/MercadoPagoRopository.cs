using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LojaAthena.Repositories;

public class MercadoPagoRopository : IMercadoPagoRepository
{
    private readonly HttpClient _httpClient;
    public MercadoPagoRopository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://api.mercadopago.com");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.GetSection("MercadoPago:SecretKey").Get<string>());
        _httpClient.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
    }
    
    public PaymentResponseDto CreatePayment(CreatePaymentRequestDto paymentDto)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(paymentDto));
            var response = _httpClient.PostAsync("/v1/payments", content).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<PaymentResponseDto>().Result;     /*ReadAsStringAsync().GetAwaiter().GetResult();*/
                return result;
            }
            else
            {
                var errorResult = response.Content.ReadAsAsync<PaymentResponseDto>().Result;
                return errorResult;
            }
        }
        catch (Exception message)
        {
            throw new Exception(message.Message);
        }
    }
    /*[HttpPost]
    public PaymentResponseDto CreatePayment([FromBody] CreatePaymentDto paymentDto)
    {
        if (paymentDto == null)
        {
            throw new ArgumentNullException(nameof(paymentDto), "PaymentDto cannot be null.");
        }

        try
        {
            var json = JsonConvert.SerializeObject(paymentDto);
            Console.WriteLine(json); // Ou use um logger apropriado

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("/v1/payments", content).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<PaymentResponseDto>().Result;
                return result;
            }
            else
            {
                var errorResult = response.Content.ReadAsStringAsync().Result; // Obtenha o conteúdo completo da resposta
                Console.WriteLine(errorResult); // Ou use um logger apropriado
                throw new ApplicationException($"Erro na solicitação: {errorResult}");
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw new Exception(ex);
        }
    }*/
}
