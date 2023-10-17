//using Amazon.S3.Model;
//using APPLICATION.Shared.Domain.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace UnitTest.API.Controllers
//{
//    public class FilesHandlerTest
//    {
//        private readonly Mock<IS3UploadService> _s3UploadService;
//        private readonly FilesHandler _handler;

//        public FilesHandlerTest()
//        {
//            _s3UploadService = new Mock<IS3UploadService>();
//            _handler = new FilesHandler(_s3UploadService.Object);
//        }

//        [Fact]
//        public async Task FilesHandler_Handle_WhenPayloadFilesInfoRequest_ShouldReturnFilesListOk()
//        {
//            var expected = new FilesInfoResponse(new List<string>() { "" });

//            _s3UploadService.Setup(e => e.ListFilesAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
//                .ReturnsAsync(new List<S3Object>() { new S3Object() { Key = "" } });

//            var task = _handler.Handle(new FilesInfoRequest(), default);

//            var result = await task;

//            Assert.NotNull(result);
//            Assert.Equal(expected.Files, result.Files);
//        }

//        [Fact]
//        public async Task FilesHandler_Handle_WhenPayloadFilesInfoRequestErr_ShouldReturnNull()
//        {
//            var expected = new FilesInfoResponse(new List<string>() { "" });

//            var task = _handler.Handle(new FilesInfoRequest(), default);

//            var result = await task;

//            Assert.Null(result);
//        }

//        [Fact]
//        public async Task FilesHandler_Handle_WhenPayloadFileUploaderRequest_ShouldReturnFileNameInBucket()
//        {
//            var expected = "202206051940-calc";
//            var formFile = new Mock<IFormFile>();

//            _s3UploadService.Setup(e => e.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()))
//             .ReturnsAsync(() => expected);

//            var task = _handler.Handle(new FileUploader(formFile.Object), default);

//            var result = await task;

//            Assert.NotNull(result);
//            Assert.Equal(expected, result);
//        }

//        [Fact]
//        public async Task FilesHandler_Handle_WhenPayloadFileUploaderErr_ShoulFalse()
//        {
//            var formFile = new Mock<IFormFile>();

//            _s3UploadService.Setup(e => e.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()))
//             .Throws<Exception>();

//            var task = _handler.Handle(new FileUploader(formFile.Object), default);

//            var result = await task;

//            Assert.Null(result);
//        }
//    }
//}