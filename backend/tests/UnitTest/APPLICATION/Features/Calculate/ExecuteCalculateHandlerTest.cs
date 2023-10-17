//using APPLICATION.Shared.Repositories.CarbonSourceProjectItem;
//using Moq;
//using System.Threading.Tasks;
//using Xunit;

//namespace UnitTest.API.Controllers
//{
//    public class ExecuteCalculateHandlerTest
//    {
//        private readonly ExecuteCalculateHandler _handler;
//        private readonly Mock<ICarbonSourceProjectItemRepository> _projectItemRepository;

//        public ExecuteCalculateHandlerTest()
//        {
//            _projectItemRepository = new();
//            //_handler = new ExecuteCalculateHandler(_projectItemRepository.Object);
//        }

//        [Theory]
//        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 110, 3, 147, 3)]
//        [InlineData(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 8, 3, 9, 3)]
//        [InlineData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
//        [InlineData(5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 160, 35, 185, 35)]
//        public async Task CalculateController_Calculate_WhenEmptyRequest_ShouldResultNotfound(
//            decimal energy,
//            decimal effluents,
//            decimal refrigerantGases,
//            decimal fuel,
//            decimal waste,
//            decimal compost,
//            decimal transportLoadE,
//            decimal transportLoadS,
//            decimal transportPeople,
//            decimal solarPanels,
//            decimal heatTreatment,
//            decimal wasteRecycled,
//            decimal manual,
//            decimal automatic,

//            decimal manualTotal,
//            decimal manualAvoided,
//            decimal automaticTotal,
//            decimal automaticAvoited
//            )
//        {
//            var expected = new CalculateResponse()
//            {
//                Avoided = manualAvoided,
//                Total = manualTotal
//            };

//            var task = _handler.Handle(new()
//            {
//                Manual = new()
//                {
//                    Percentil = manual,
//                    Compost = compost,
//                    Effluent = effluents,
//                    Energy = energy,
//                    Fuel = fuel,
//                    HeatTreatment = heatTreatment,
//                    RefrigerantGas = refrigerantGases,
//                    SolarPanels = solarPanels,
//                    TransportLoadE = transportLoadE,
//                    TransportLoadS = transportLoadS,
//                    TransportPeople = transportPeople,
//                    WasteRecycled = wasteRecycled,
//                    Waste = waste
//                }
//            }
//            , default);

//            var result = await task;

//            Assert.NotNull(result);
//            //Assert.Equal(expected.Avoided, result.Avoided);
//        }
//    }
//}