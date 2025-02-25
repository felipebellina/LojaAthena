using LojaAthena.Models;
using LojaAthena.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

    public PaymentResponseDto CreatePayment([FromBody]CreatePaymentDto paymentDto)
    {

        var content = new StringContent(JsonConvert.SerializeObject(paymentDto));
        var response = _httpClient.PostAsync("/v1/payments", content).GetAwaiter().GetResult();

        if (response.IsSuccessStatusCode)
        {
            var result = response.Content.ReadAsAsync<PaymentResponseDto>().Result;

            return result;
        }
        else
        {
            var errorResult = response.Content.ReadAsAsync<PaymentResponseDto>().Result;
            return errorResult;
        }

    }
}
