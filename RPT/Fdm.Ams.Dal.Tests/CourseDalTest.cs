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
    public class CourseDalTest
    {
        private readonly string baseAddress;
        private readonly string courseControllerRoute;
        private string conflictedMessage = string.Empty;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<HttpClient> mockHttpClient;
        private Mock<IHttpClientWrapper> mockHttpClientWrapper;
        private Mock<IGenericMessageHelper> mockMessageHelper;
        private Mock<IResponseStatusCodeHelper> mockResponseStatusCodeHelper;
        private HttpResponseMessage responseMessage;

        public CourseDalTest()
        {
            mockHttpClient = new Mock<HttpClient>();
            mockConfiguration = new Mock<IConfiguration>();
            baseAddress = "https:fakehost";
            courseControllerRoute = "/rpt/pathway";
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
            conflictedMessage = "[Conflicted] This course is assigned to one of the functionality and cannot be deleted.";
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(courseControllerRoute)).Returns(conflictedMessage);
            var dal = CreateCourseDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockMessageHelper.Verify(x => x.CreateGenericConflictMessage(courseControllerRoute), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallHttpClientWrapperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateCourseDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallResponseStatusCodeHelperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            int Id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage((System.Net.HttpStatusCode)statusCode);
            conflictedMessage = "[Conflicted] This course is assigned to one of the functionality and cannot be deleted.";

            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{Id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(courseControllerRoute)).Returns(conflictedMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateCourseDal();
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
            var courseType = Builder<CourseType>.CreateNew().Build();
            var innerException = "[Conflicted] This course is assigned to one of the functionality and cannot be deleted.";
            conflictedMessage = innerException;

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };
            var responseBody = responseMessage.Content.ReadAsStringAsync();

            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(courseControllerRoute)).Returns(conflictedMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));
            var dal = CreateCourseDal();
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
            var course = Builder<Course>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            var dal = CreateCourseDal();
            //Act
            await dal.PostAsync(course);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldCallResponseStatusCodeHelperPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            var course = Builder<Course>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);

            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateCourseDal();
            //Act
            await dal.PostAsync(course);
            //Assert
            mockResponseStatusCodeHelper.Verify(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnCourse_WhenCalled()
        {
            //Arrange
            var course = Builder<Course>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(course), null, "application/json")
            };

            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateCourseDal();
            //Act
            var result = await dal.PostAsync(course);
            //Assert
            result.Should().BeEquivalentTo(course);
        }

        [Fact]
        public async Task PostAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadRequest;
            var course = Builder<Course>.CreateNew().Build();
            var innerException = "Duplicate values cannot be inserted. Please check all fields again before inserting the value.";

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };

            var responseBody = responseMessage.Content.ReadAsStringAsync();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));

            var dal = CreateCourseDal();
            //Act
            Func<Task> result = async () => await dal.PostAsync(course);
            //Assert
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage(responseBody.ToString());
        }

        #endregion PostAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_CallsHttpClientWrapperPutAsyncOnce_WhenCalled()
        {
            //Arrange
            var input = Builder<Course>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{input.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateCourseDal();
            //Act
            await dal.PutAsync(input);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{input.Id}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            var input = Builder<Course>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(input), null, "application/json")
            };
            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object
                , $"{baseAddress}{courseControllerRoute}/{input.Id}"
                , It.IsAny<StringContent>()))
                .ReturnsAsync(responseMessage);
            var dal = CreateCourseDal();
            //Act
            try
            {
                await dal.PutAsync(input);
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

        #endregion PutAsync

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_CallsHttpClientWrapperGetAsyncOnce_WhenCalled()
        {
            //Arrange
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}")).ReturnsAsync(responseMessage);

            var dal = CreateCourseDal();
            //Act
            await dal.GetAllAsync();
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}"), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfCourseFromClient_WhenSuccessful()
        {
            //Arrange
            var list = Builder<Course>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateCourseDal();
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
            var list = Builder<Course>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateCourseDal();
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
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateCourseDal();
            //Act
            await dal.GetByIdAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCourseFromResponseMessage_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var course = Builder<Course>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(course), null, "application/json");
            var dal = CreateCourseDal();
            //Act
            var result = await dal.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(course);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            int id = 1;
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{courseControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateCourseDal();

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

        private CourseDal CreateCourseDal()
        {
            return new CourseDal(mockHttpClient.Object, mockConfiguration.Object, mockHttpClientWrapper.Object, mockResponseStatusCodeHelper.Object, mockMessageHelper.Object);
        }
    }
}