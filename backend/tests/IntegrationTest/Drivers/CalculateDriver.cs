//using Microsoft.Extensions.Configuration;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Text.Json;

//namespace FuncionalTest.Drivers
//{
//    public class CalculateDriver : IDisposable
//    {
//        private readonly HttpClient httpClient;

//        public CalculateDriver(IConfiguration configuration)
//        {
//            httpClient = new HttpClient();
//            httpClient.BaseAddress = new Uri(configuration["IntegrationTests:BaseURL"]);
//        }

//        public async Task<string> Upload(string fileName)
//        {
//            using (var multipartFormContent = new MultipartFormDataContent())
//            {
//                var fileStreamContent = new StreamContent(File.OpenRead(fileName));
//                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");

//                multipartFormContent.Add(fileStreamContent, name: "file", fileName: fileName);

//                var response = await httpClient.PostAsync("/upload", multipartFormContent);
//                response.EnsureSuccessStatusCode();

//                var resultContent = response.Content.ReadAsStringAsync();
//                return await resultContent;
//            }
//        }

//        public async Task<CalculateRequest> Process(string fileName)
//        {
//            var response = await httpClient.GetAsync("/process?filename=" + fileName);
//            response.EnsureSuccessStatusCode();

//            if (response.IsSuccessStatusCode)
//            {
//                var resultContent = response.Content.ReadAsStringAsync();
//                var jsonString = await resultContent;
//                var item1 = (jsonString.Split(",\"item2")[0]).Replace("{\"item1\":", "");

//                var result = (CalculateRequest)JsonSerializer.Deserialize(item1, typeof(CalculateRequest), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

//                return result;
//            }
//            else
//            {
//                return new();
//            }
//        }

//        public async Task<CalculateResponse> Calculate(CalculateRequest itensCalculate)
//        {
//            var json = JsonSerializer.Serialize(itensCalculate);
//            var data = new StringContent(json, Encoding.UTF8, "application/json");

//            var response = await httpClient.PostAsync("/calculate", data);
//            response.EnsureSuccessStatusCode();

//            var resultContent = response.Content.ReadAsStringAsync();
//            var jsonResponse = await resultContent;

//            var result = (CalculateResponse)JsonSerializer.Deserialize(jsonResponse, typeof(CalculateResponse), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

//            return result;
//        }

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}