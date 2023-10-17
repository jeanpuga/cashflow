//using APPLICATION.Shared.Domain.Interfaces;
//using Moq;
//using System.IO;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace UnitTest.API.Controllers
//{
//    public class ProccesArchiveHandlerTest
//    {
//        private readonly ProccesArchiveHandler _handler;
//        private readonly Mock<IS3UploadService> _s3UploadService;
//        private readonly Mock<IExcelService> _excelService;

//        public ProccesArchiveHandlerTest()
//        {
//            _s3UploadService = new Mock<IS3UploadService>();
//            _excelService = new Mock<IExcelService>();

//            _handler = new ProccesArchiveHandler(_s3UploadService.Object, _excelService.Object);
//        }

//        [Fact]
//        public async Task ProccesArchiveHandler_Handle_WhenPayloadProccesRequest_ShouldResultNullSuccess()
//        {
//            var task = _handler.Handle(new ProccesRequest(""), default);

//            var result = await task;

//            Assert.Null(result);
//            _s3UploadService.Verify(e => e.ReadFileAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());
//            _excelService.Verify(e => e.ReadAsync(It.IsAny<MemoryStream>()), Times.Once());
//        }
//    }
//}