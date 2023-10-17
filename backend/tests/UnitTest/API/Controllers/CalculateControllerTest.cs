//using API.Controllers;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace UnitTest.API.Controllers
//{
//    public class OperationsControllerTest
//    {
//        private readonly Mock<IMediator> _mediator;
//        private readonly OperationsController _controller;

//        public OperationsControllerTest()
//        {
//            _mediator = new Mock<IMediator>();
//            _controller = new OperationsController(_mediator.Object);
//        }

//        [Fact]
//        public async Task OperationsController_Calculate_WhenEmptyRequest_ShouldResultNotfound()
//        {
//            var result = await _controller.Calculate(null, default);

//            Equals("404", ((NotFoundResult)result).StatusCode);
//        }

//        [Fact]
//        public async Task OperationsController_Calculate_WhenPayloadWrong_ShouldResultBadRequest()
//        {
//            var mediator = new Mock<IMediator>();
//            var controller = new OperationsController(mediator.Object);

//            mediator.SetupSequence(e => e.Send(It.IsAny<CalculateRequest>(), It.IsAny<CancellationToken>()))
//                    .ThrowsAsync(new Exception());

//            var result = await controller.Calculate(new CalculateRequest(), default);

//            Equals("500", ((ObjectResult)result).StatusCode);
//        }

//        [Fact]
//        public async Task OperationsController_Calculate_WhenPayloadCorrect_ShouldResultOk()
//        {
//            var result = await _controller.Calculate(new(), default);

//            Equals("200", ((OkObjectResult)result).StatusCode);
//        }
//    }
//}