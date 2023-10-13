using Fdm.Ams.Common;
using Fdm.Ams.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Dal.Tests
{
    public class CourseModuleTemplateDalTest
    {
        private readonly string baseAddress;
        private readonly string courseModuleTemplateControllerRoute;
        private string conflictedMessage = string.Empty;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<HttpClient> mockHttpClient;
        private Mock<IHttpClientWrapper> mockHttpClientWrapper;
        private Mock<IGenericMessageHelper> mockMessageHelper;
        private Mock<IResponseStatusCodeHelper> mockResponseStatusCodeHelper;
        private HttpResponseMessage responseMessage;

        public CourseModuleTemplateDalTest()
        {
            mockHttpClient = new Mock<HttpClient>();
            mockConfiguration = new Mock<IConfiguration>();
            baseAddress = "https:fakehost";
            courseModuleTemplateControllerRoute = "/rpt/course-templates";
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
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(courseModuleTemplateControllerRoute)).Returns(conflictedMessage);
            var dal = CreateCourseModuleTemplateDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockMessageHelper.Verify(x => x.CreateGenericConflictMessage(courseModuleTemplateControllerRoute), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallHttpClientWrapperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateCourseModuleTemplateDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallResponseStatusCodeHelperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            int Id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage((System.Net.HttpStatusCode)statusCode);
            conflictedMessage = "[Conflicted] This courseModule-template is assigned to one of the functionality and cannot be deleted.";

            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{Id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(courseModuleTemplateControllerRoute)).Returns(conflictedMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateCourseModuleTemplateDal();
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
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateNew().Build();
            var innerException = "[Conflicted] This courseModule-template is assigned to one of the functionality and cannot be deleted.";
            conflictedMessage = innerException;

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };
            var responseBody = responseMessage.Content.ReadAsStringAsync();

            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(courseModuleTemplateControllerRoute)).Returns(conflictedMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));
            var dal = CreateCourseModuleTemplateDal();
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
            var model = Builder<CourseModuleTemplate>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            var dal = CreateCourseModuleTemplateDal();
            //Act
            await dal.PostAsync(model);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldCallResponseStatusCodeHelperPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);

            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateCourseModuleTemplateDal();
            //Act
            await dal.PostAsync(courseModuleTemplate);
            //Assert
            mockResponseStatusCodeHelper.Verify(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnCourseModuleTemplate_WhenCalled()
        {
            //Arrange
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(courseModuleTemplate), null, "application/json")
            };

            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateCourseModuleTemplateDal();
            //Act
            var result = await dal.PostAsync(courseModuleTemplate);
            //Assert
            result.Should().BeEquivalentTo(courseModuleTemplate);
        }

        [Fact]
        public async Task PostAsync_ShouldThrowHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadRequest;
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateNew().Build();
            var innerException = "Duplicate values cannot be inserted. Please check all fields again before inserting the value.";

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };

            var responseBody = responseMessage.Content.ReadAsStringAsync();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));

            var dal = CreateCourseModuleTemplateDal();
            //Act
            Func<Task> result = async () => await dal.PostAsync(courseModuleTemplate);
            //Assert
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage(responseBody.ToString());
        }

        #endregion PostAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallHttpClientWrapperPutAsyncOnce_WhenCalled()
        {
            //Arrange
            var input = Builder<CourseModuleTemplate>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{input.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateCourseModuleTemplateDal();
            //Act
            await dal.PutAsync(input);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{input.Id}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldCallResponseStatusCodeHelperPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{courseModuleTemplate.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateCourseModuleTemplateDal();
            //Act
            await dal.PutAsync(courseModuleTemplate);

            //Assert
            mockResponseStatusCodeHelper.Verify(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnCourseModuleTemplate_WhenCalled()
        {
            //Arrange
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(courseModuleTemplate), null, "application/json")
            };

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{courseModuleTemplate.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateCourseModuleTemplateDal();
            //Act
            var result = await dal.PutAsync(courseModuleTemplate);
            //Assert
            result.Should().BeEquivalentTo(courseModuleTemplate);
        }

        [Fact]
        public async Task PutAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadRequest;
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateNew().Build();
            var innerException = "Duplicate values cannot be updated. Please check all fields again before inserting the value.";

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };
            var responseBody = responseMessage.Content.ReadAsStringAsync();

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object
                            , $"{baseAddress}{courseModuleTemplateControllerRoute}/{courseModuleTemplate.Id}"
                            , It.IsAny<StringContent>()))
                            .ReturnsAsync(responseMessage);

            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));
            var dal = CreateCourseModuleTemplateDal();
            //Act
            Func<Task> result = async () => await dal.PutAsync(courseModuleTemplate);
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
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}")).ReturnsAsync(responseMessage);

            var dal = CreateCourseModuleTemplateDal();
            //Act
            await dal.GetAllAsync();
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}"), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfCourseModuleTemplateFromClient_WhenSuccessful()
        {
            //Arrange
            var list = Builder<CourseModuleTemplate>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateCourseModuleTemplateDal();
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
            var list = Builder<CourseModuleTemplate>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateCourseModuleTemplateDal();
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
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateCourseModuleTemplateDal();
            //Act
            await dal.GetByIdAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCourseModuleTemplateFromResponseMessage_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var courseModuleTemplate = Builder<CourseModuleTemplate>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(courseModuleTemplate), null, "application/json");
            var dal = CreateCourseModuleTemplateDal();
            //Act
            var result = await dal.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(courseModuleTemplate);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            int id = 1;
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseModuleTemplateControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateCourseModuleTemplateDal();

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

        private CourseModuleTemplateDal CreateCourseModuleTemplateDal()
        {
            return new CourseModuleTemplateDal(mockHttpClient.Object, mockConfiguration.Object, mockHttpClientWrapper.Object, mockResponseStatusCodeHelper.Object, mockMessageHelper.Object);
        }
    }
}