using Fdm.Common.Service;
using Fdm.Common.Web;
using Fdm.Data.ResourcePlanningTool.Models;
using Fdm.ResourcePlanningTool.Dtos;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Dates;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Common.Tests.Web
{
    public abstract class BaseControllerTests<TGenericService, TDto, TPostDto>

        where TGenericService : class, IGenericService<TDto, TPostDto>
        where TDto : class, IGenericDto
        where TPostDto : class
    {
        private Mock<ILogger<TDto>> mockLogger;
        private Mock<TGenericService> mockService;

        protected BaseControllerTests()
        {
            mockLogger = new Mock<ILogger<TDto>>();
            mockService = new Mock<TGenericService>();
        }

        #region Delete

        [Theory]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        public async Task Delete_ShouldCallGetByIdNoTrackingOnce_WhenCalled(int id)
        {
            //Arrange
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            await controller.Delete(id);
            //Assert
            mockService.Verify(x => x.GetByIdNoTracking(id), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldCallServiceDeleteOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            var item = Builder<TDto>.CreateNew()
                         .With(x => x.Id == 0).Build();
            mockService.Setup(x => x.GetByIdNoTracking(id)).ReturnsAsync(item);
            //Act
            await controller.Delete(id);
            //Assert
            mockService.Verify(x => x.Delete(id), Times.Once);
        }

       
        [Fact]
        public async Task Delete_ShouldReturnOkResult_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = BaseControllerTestHelper(mockService, mockLogger);

            var item = Builder<TDto>.CreateNew()
                         .With(x => x.Id == 0).Build();
            mockService.Setup(x => x.GetByIdNoTracking(id)).ReturnsAsync(item);

            //Act
            var result = await controller.Delete(id);
            //Assert
            result.Should().BeOfType<OkResult>();
        }
        [Fact]
        public async Task Delete_ShouldReturnWithNotFoundResult404StatusCode_WhenTheRecordDoesNotExist()
        {
            //Arrange
            var dto = Builder<TDto>.CreateNew().With(x => x.Id).Build();
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            mockService.Setup(x => x.Delete(dto.Id));
            //Act
            ActionResult<TDto> deleteResult = await controller.Delete(dto.Id);

            //Assert
            deleteResult.Result.Should().BeOfType<NotFoundObjectResult>();
        }
       


        #endregion Delete

        #region Get (All)

        [Fact]
        public async Task GetAll_CallsServiceGetAllOnce_WhenCalled()
        {
            //Arrange
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            await controller.Get();
            //Assert
            mockService.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnActionResultWithIEnumerableOfTDtos_WhenCalled()
        {
            //Arrange
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            var result = await controller.Get();
            //Assert
            result.Should().BeOfType<ActionResult<IEnumerable<TDto>>>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnResultFromService_WhenCalled()
        {
            //Arrange
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            var dtoList = Builder<TDto>.CreateListOfSize(3).Build();
            mockService.Setup(x => x.GetAll()).ReturnsAsync(dtoList);
            //Act
            var returned = await controller.Get();
            //Assert
            returned.Value.Should().BeEquivalentTo(dtoList);
        }

        #endregion Get (All)

        #region Get (By Id)

        [Fact]
        public async Task GetById_ShouldCallServiceGetByIdOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            await controller.Get(id);
            //Assert
            mockService.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact]
        public async Task GetById_ShouldReturnsActionResultWithTDto_WhenCalled()
        {
            //Arrange
            int id = 1;
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            var result = await controller.Get(id);
            //Assert
            result.Should().BeOfType<ActionResult<TDto>>();
        }

        [Fact]
        public async Task GetById_ShouldReturnsNotFoundResult_WhenEntityDoesNotExist()
        {
            //Arrange
            int id = 1;
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            var returned = await controller.Get(id);
            //Assert
            returned.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetById_ShouldReturnsResultFromService_WhenEntityExists()
        {
            //Arrange
            int id = 1;
            var dto = Builder<TDto>.CreateNew().Build();
            mockService.Setup(x => x.GetById(id)).ReturnsAsync(dto);
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            var returned = await controller.Get(id);
            //Assert
            returned.Value.Should().BeSameAs(dto);
        }

        #endregion Get (By Id)

        #region Post

        [Fact]
        public async Task Post_ShouldCallServiceCreateOnce_WhenCalled()
        {
            //Arrange
            var postDto = Builder<TPostDto>.CreateNew().Build();
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            await controller.Post(postDto);
            //Assert
            mockService.Verify(x => x.Create(postDto), Times.Once);
        }

        [Fact]
        public async Task Post_ShouldReturnsTDto_WhenSuccessful()
        {
            //Arrange
            var postDto = Builder<TPostDto>.CreateNew().Build();
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            var result = await controller.Post(postDto);
            //Assert
            result.Should().BeOfType<ActionResult<TDto>>();
        }

        [Fact]
        public async Task Post_ShouldReturnWithNotFoundResult404StatusCode_WhenTheRecordDoesNotExist()
        {
            //Arrange
            var postDto = Builder<TPostDto>.CreateNew().Build();
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            mockService.Setup(x => x.Create(postDto));
            //Act
            ActionResult<TDto> postResult = await controller.Post(postDto);

            //Assert
            postResult.Result.Should().BeOfType<NotFoundObjectResult>();
        }
      
       

        #endregion Post

        #region Put

        [Fact]
        public async Task Put_ShouldCallServiceUpdateOnce_WhenCalled()
        {
            //Arrange
            int id = 5;
            var dto = Builder<TDto>.CreateNew().With(x => x.Id).Build();
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            mockService.Setup(x => x.GetByIdNoTracking(id)).ReturnsAsync(dto);
            mockService.Setup(x => x.Update(dto)).ReturnsAsync(dto);
            //Act
            await controller.Put(id, dto);
            //Assert
            mockService.Verify(x => x.Update(dto), Times.Once);
        }

        [Fact]
        public async Task Put_ShouldReturnsTDto_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var dto = Builder<TDto>.CreateNew().With(x => x.Id).Build();
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            //Act
            var result = await controller.Put(id, dto);
            //Assert
            result.Should().BeOfType<ActionResult<TDto>>();
        }

        [Fact]
        public async Task Put_ShouldReturnWithNotFoundResult404StatusCode_WhenTheRecordDoesNotExist()
        {
            //Arrange
            int id = 5;
            var dto = Builder<TDto>.CreateNew().With(x => x.Id).Build();
            var controller = BaseControllerTestHelper(mockService, mockLogger);
            mockService.Setup(x => x.Update(dto)).ReturnsAsync(dto);
            //Act
            ActionResult<TDto> putResult = await controller.Put(id, dto);
          
            //Assert
           putResult.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        #endregion Put

        protected abstract BaseController<TGenericService, TDto, TPostDto> BaseControllerTestHelper(Mock<TGenericService> mockService, Mock<ILogger<TDto>> mockLogger);
    }
}