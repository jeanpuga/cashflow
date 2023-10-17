//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace UnitTest.API.Controllers
//{
//    public class MaintenanceControllerTest
//    {
//        private readonly Mock<IMediator> _mediator;
//        private readonly MaintenanceController _controller;

//        public MaintenanceControllerTest()
//        {
//            _mediator = new Mock<IMediator>();
//            _controller = new MaintenanceController(_mediator.Object);
//        }

//        [Fact]
//        public async Task MaintenanceController_Upload_WhenEmptyRequest_ShouldResultNotfound()
//        {
//            var result = await _controller.Upload(null, default);

//            Equals("204", ((ObjectResult)result).StatusCode);
//        }

//        [Fact]
//        public async Task MaintenanceController_Upload_WhenPayloadException_ShouldResultBadRequest()
//        {
//            var mediator = new Mock<IMediator>();
//            var fileMock = new Mock<IFormFile>();
//            var controller = new MaintenanceController(mediator.Object);

//            mediator.SetupSequence(e => e.Send(It.IsAny<FileUploader>(), It.IsAny<CancellationToken>()))
//                    .ThrowsAsync(new Exception());

//            var result = await controller.Upload(fileMock.Object, default);

//            Equals("500", ((ObjectResult)result).StatusCode);
//        }

//        [Fact]
//        public async Task MaintenanceController_Upload_WhenPayloadWrong_ShouldResultNotExpected()
//        {
//            var mediator = new Mock<IMediator>();
//            var fileMock = new Mock<IFormFile>();
//            var controller = new MaintenanceController(mediator.Object);

//            mediator.SetupSequence(e => e.Send(It.IsAny<FileUploader>(), It.IsAny<CancellationToken>()))
//                    .ReturnsAsync("");

//            var result = await controller.Upload(fileMock.Object, default);

//            Equals("417", ((ObjectResult)result).StatusCode);
//        }

//        [Fact]
//        public async Task MaintenanceController_Upload_WhenPayloadCorrect_ShouldResultOk()
//        {
//            var mediator = new Mock<IMediator>();
//            var fileMock = new Mock<IFormFile>();
//            var controller = new MaintenanceController(mediator.Object);

//            mediator.SetupSequence(e => e.Send(It.IsAny<FileUploader>(), It.IsAny<CancellationToken>()))
//                    .ReturnsAsync("202205061940_calc");

//            var task = controller.Upload(fileMock.Object, default);

//            var result = await task;

//            Equals("200", ((OkObjectResult)result).StatusCode);
//        }

//        [Fact]
//        public async Task MaintenanceController_Process_WhenPayloadWrong_ShouldResultBadRequest()
//        {
//            var mediator = new Mock<IMediator>();
//            var controller = new MaintenanceController(mediator.Object);

//            mediator.SetupSequence(e => e.Send(It.IsAny<CalculateRequest>(), It.IsAny<CancellationToken>()))
//                    .ThrowsAsync(new Exception());

//            var result = await controller.Process("", default);

//            Equals("500", ((ObjectResult)result).StatusCode);
//        }

//        [Fact]
//        public async Task MaintenanceController_Process_WhenPayloadCorrect_ShouldResultOk()
//        {
//            var result = await _controller.Process("", default);

//            Equals("200", ((OkObjectResult)result).StatusCode);
//        }

//        [Fact]
//        public async Task MaintenanceController_ListFiles_WhenPayloadWrong_ShouldResultBadRequest()
//        {
//            var mediator = new Mock<IMediator>();
//            var controller = new MaintenanceController(mediator.Object);

//            mediator.SetupSequence(e => e.Send(It.IsAny<CalculateRequest>(), It.IsAny<CancellationToken>()))
//                    .ThrowsAsync(new Exception());

//            var result = await controller.ListFiles(default);

//            Equals("500", ((ObjectResult)result).StatusCode);
//        }

//        [Fact]
//        public async Task MaintenanceController_ListFiles_WhenPayloadCorrect_ShouldResultOk()
//        {
//            var result = await _controller.ListFiles(default);

//            Equals("200", ((OkObjectResult)result).StatusCode);
//        }
//    }
//}