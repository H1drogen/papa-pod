using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Fdm.Ams.Common;
using Fdm.Ams.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Fdm.Ams.Dal.Tests
{
    public class CourseTemplateDalTest
    {
        private readonly string baseAddress;
        private readonly string courseTemplateControllerRoute;
        private string conflictedMessage = string.Empty;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<HttpClient> mockHttpClient;
        private Mock<IHttpClientWrapper> mockHttpClientWrapper;
        private Mock<IGenericMessageHelper> mockMessageHelper;
        private Mock<IResponseStatusCodeHelper> mockResponseStatusCodeHelper;
        private HttpResponseMessage responseMessage;

        public CourseTemplateDalTest()
        {
            mockHttpClient = new Mock<HttpClient>();
            mockConfiguration = new Mock<IConfiguration>();
            baseAddress = "https:fakehost";
            courseTemplateControllerRoute = "/rpt/pathway-templates";
            mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockResponseStatusCodeHelper = new Mock<IResponseStatusCodeHelper>();
            mockMessageHelper = new Mock<IGenericMessageHelper>();
        }


        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldCallGenericMessageHelperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            conflictedMessage = "[Conflicted] This pathway-types is assigned to one of the functionality and cannot be deleted.";
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(courseTemplateControllerRoute)).Returns(conflictedMessage);
            var dal = CreateCourseTemplateDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockMessageHelper.Verify(x => x.CreateGenericConflictMessage(courseTemplateControllerRoute), Times.Once);
        }


        [Fact]
        public async Task DeleteAsync_ShouldCallHttpClientWrapperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateCourseTemplateDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallResponseStatusCodeHelperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            int Id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage((System.Net.HttpStatusCode)statusCode);
            conflictedMessage = "[Conflicted] This course-template is assigned to one of the functionality and cannot be deleted.";

            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{Id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(courseTemplateControllerRoute)).Returns(conflictedMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateCourseTemplateDal();
            //Act
            await dal.DeleteAsync(Id);

            //Assert
            mockResponseStatusCodeHelper.Verify(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            int id = 1;
            var statusCode = HttpStatusCode.BadRequest;
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            var innerException = "[Conflicted] This course-template is assigned to one of the functionality and cannot be deleted.";
            conflictedMessage = innerException;

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };
            var responseBody = responseMessage.Content.ReadAsStringAsync();

            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(courseTemplateControllerRoute)).Returns(conflictedMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));
            var dal = CreateCourseTemplateDal();
            //Act
            Func<Task> result = async () => await dal.DeleteAsync(id);
            //Assert
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage(responseBody.ToString());
        }

        #endregion DeleteAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_ShouldCallHttpClientWrapperPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            var dal = CreateCourseTemplateDal();
            //Act
            await dal.PostAsync(courseTemplate);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldCallResponseStatusCodeHelperPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);

            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateCourseTemplateDal();
            //Act
            await dal.PostAsync(courseTemplate);
            //Assert
            mockResponseStatusCodeHelper.Verify(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnCourseTemplate_WhenCalled()
        {
            //Arrange
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(courseTemplate), null, "application/json")
            };

            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateCourseTemplateDal();
            //Act
            var result = await dal.PostAsync(courseTemplate);
            //Assert
            result.Should().BeEquivalentTo(courseTemplate);
        }

        [Fact]
        public async Task PostAsync_ShouldThrowHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadRequest;
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            var innerException = "Duplicate values cannot be inserted. Please check all fields again before inserting the value.";

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };

            var responseBody = responseMessage.Content.ReadAsStringAsync();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));

            var dal = CreateCourseTemplateDal();
            //Act
            Func<Task> result = async () => await dal.PostAsync(courseTemplate);
            //Assert
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage(responseBody.ToString());
        }

        #endregion PostAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallHttpClientWrapperPutAsyncOnce_WhenCalled()
        {
            //Arrange
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{courseTemplate.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateCourseTemplateDal();
            //Act
            await dal.PutAsync(courseTemplate);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{courseTemplate.Id}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldCallResponseStatusCodeHelperPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{courseTemplate.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateCourseTemplateDal();
            //Act
            await dal.PutAsync(courseTemplate);

            //Assert
            mockResponseStatusCodeHelper.Verify(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnCourseTemplate_WhenCalled()
        {
            //Arrange
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(courseTemplate), null, "application/json")
            };

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{courseTemplate.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateCourseTemplateDal();
            //Act
            var result = await dal.PutAsync(courseTemplate);
            //Assert
            result.Should().BeEquivalentTo(courseTemplate);
        }

        [Fact]
        public async Task PutAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadRequest;
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();
            var innerException = "Duplicate values cannot be updated. Please check all fields again before inserting the value.";

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };
            var responseBody = responseMessage.Content.ReadAsStringAsync();

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object
                            , $"{baseAddress}{courseTemplateControllerRoute}/{courseTemplate.Id}"
                            , It.IsAny<StringContent>()))
                            .ReturnsAsync(responseMessage);

            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));
            var dal = CreateCourseTemplateDal();
            //Act
            Func<Task> result = async () => await dal.PutAsync(courseTemplate);
            //Assert
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage(responseBody.ToString());
        }


        #endregion PutAsync

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsHttpClientWrapperGetAsyncOnce_WhenCalled()
        {
            //Arrange
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}")).ReturnsAsync(responseMessage);

            var dal = CreateCourseTemplateDal();
            //Act
            await dal.GetAllAsync();
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}"), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfCourseTemplateFromClient_WhenSuccessful()
        {
            //Arrange
            var list = Builder<CourseTemplate>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateCourseTemplateDal();
            //Act
            var result = await dal.GetAllAsync();
            //Assert
            result.Should().BeEquivalentTo(list);
        }

        [Fact]
        public async Task GetAllAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            var list = Builder<CourseTemplate>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateCourseTemplateDal();
            //Act
            try
            {
                await dal.GetAllAsync();
            }
            catch (Exception e)
            {
                //Assert
                e.Should().BeOfType<HttpRequestException>();
                e.As<HttpRequestException>().StatusCode.Should().Be(statusCode);
                return;
            }

            Assert.False(true, "No Exceptions were thrown");
        }

        #endregion GetAllAsync

        #region GetByIdAsync

        [Fact]
        public async Task GetByIdAsync_CallsHttpClientWrapperGetAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateCourseTemplateDal();
            //Act
            await dal.GetByIdAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCourseTemplateFromResponseMessage_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var courseTemplate = Builder<CourseTemplate>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(courseTemplate), null, "application/json");
            var dal = CreateCourseTemplateDal();
            //Act
            var result = await dal.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(courseTemplate);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            int id = 1;
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateCourseTemplateDal();

            //Act
            try
            {
                await dal.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                //Assert
                e.Should().BeOfType<HttpRequestException>();
                e.As<HttpRequestException>().StatusCode.Should().Be(statusCode);
                return;
            }
            Assert.False(true, "No Exceptions were thrown");
        }

        #endregion GetByIdAsync

        private CourseTemplateDal CreateCourseTemplateDal()
        {
            return new CourseTemplateDal(mockHttpClient.Object, mockConfiguration.Object, mockHttpClientWrapper.Object, mockResponseStatusCodeHelper.Object, mockMessageHelper.Object);
        }
    }
}