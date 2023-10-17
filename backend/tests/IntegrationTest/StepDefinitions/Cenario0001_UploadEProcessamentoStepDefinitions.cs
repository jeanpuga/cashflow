//using FuncionalTest.Drivers;
//using FuncionalTest.Support;
//using Microsoft.Extensions.Configuration;
//using System.Globalization;

//namespace FuncionalTest.StepDefinitions
//{
//    [Binding]
//    public class Cenario0001_UploadEProcessamentoStepDefinitions
//    {
//        private readonly CalculateDriver calculateDriver;
//        private string Planilha { get; set; }
//        private string Filename { get; set; }
//        private CalculateRequest ItemsRequest { get; set; }

//        private readonly IConfiguration _configuration;

//        public Cenario0001_UploadEProcessamentoStepDefinitions()
//        {
//            File.Copy("../../../../../src/API/appsettings.json", "./appsettings.json", true);

//            if (_configuration == null)
//            {
//                _configuration = new ConfigurationBuilder()
//                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                    .Build();
//            }

//            calculateDriver = new CalculateDriver(_configuration);
//        }

//        [Given(@"a planilha preenchida ""([^""]*)""")]
//        public async Task DadoAPlanilhaPreenchida(string planilha)
//        {
//            Planilha = FuncionalTest.Support.FileHelper.GetPath(planilha);
//        }

//        [When(@"realizo upload da planilha")]
//        public async Task QuandoRealizoUploadDaPlanilha()
//        {
//            Filename = await calculateDriver.Upload(Planilha);
//        }

//        [When(@"depois executo o processo")]
//        public async Task EDepoisExecutoOProcesso()
//        {
//            ItemsRequest = await calculateDriver.Process(Filename);
//        }

//        [Then(@"deve apresentar os valores calculados")]
//        public async Task EntãoDeveApresentarOsValoresCalculados(Table table)
//        {
//            var task = calculateDriver.Calculate(ItemsRequest);
//            var dict = table.ToDictionary();
//            var provider = new CultureInfo("en-US");
//            var expected = new CalculateResponse()
//            {
//                Avoided = decimal.Parse(dict["ManualAvoided"], provider),
//                Total = decimal.Parse(dict["ManualTotal"], provider),
//            };

//            var result = await task;

//            Assert.NotNull(result);
//            Assert.Equal(expected.Total, result.Total);
//            Assert.Equal(expected.Avoided, result.Avoided);
//        }
//    }
//}