using Newtonsoft.Json;
using RestSharp;
using Super3.Domain.Model;
using Super3.Domain.Model.ModelViaCep;

namespace Super3.Domain.Services
{
    public static class ViaCepService
    {
        public static async Task GetCepInfo(Customer customer)
        {
            var client = new RestClient("http://viacep.com.br");
            var request = new RestRequest($"ws/{customer.CEP}/json", Method.Get);
            var response = await client.ExecuteAsync(request);
            var viaCepResponse = JsonConvert.DeserializeObject<CustomerViaCep>(response.Content);

            customer.Street = viaCepResponse.logradouro;
            customer.Neighborhood = viaCepResponse.bairro;
            customer.City = viaCepResponse.localidade;
            customer.Province = viaCepResponse.uf;


            if (customer.City == null)
            {
                string errorMessage = ("Invalid CEP");
                throw new Exception (errorMessage);
            }
                
        }


       
    }
}
