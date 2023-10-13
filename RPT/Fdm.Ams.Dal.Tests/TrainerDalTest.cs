using Fdm.Ams.Common;
using Fdm.Ams.Models;
using FizzWare.NBuilder;
using FluentAssertions;
using FluentAssertions.Equivalency.Tracing;
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
    public class TrainerDalTest
    {
        private readonly string baseAddress;
        private readonly string trainerControllerRoute;
        private string conflictedMessage = string.Empty;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<HttpClient> mockHttpClient;
        private Mock<IHttpClientWrapper> mockHttpClientWrapper;
        private Mock<IGenericMessageHelper> mockMessageHelper;
        private Mock<IResponseStatusCodeHelper> mockResponseStatusCodeHelper;
        private HttpResponseMessage responseMessage;

        public TrainerDalTest()
        {
            mockHttpClient = new Mock<HttpClient>();
            mockConfiguration = new Mock<IConfiguration>();
            baseAddress = "https:fakehost";
            trainerControllerRoute = "/rpt/trainers";
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
            conflictedMessage = "[Conflicted] This trainer is assigned to one of the functionality and cannot be deleted.";
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(trainerControllerRoute)).Returns(conflictedMessage);
            var dal = CreateTrainerDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockMessageHelper.Verify(x => x.CreateGenericConflictMessage(trainerControllerRoute), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallHttpClientWrapperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateTrainerDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallResponseStatusCodeHelperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            int Id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage((System.Net.HttpStatusCode)statusCode);
            conflictedMessage = "[Conflicted] This trainer is assigned to one of the functionality and cannot be deleted.";

            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{Id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(trainerControllerRoute)).Returns(conflictedMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateTrainerDal();
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
            var trainer = Builder<Trainer>.CreateNew().Build();
            var innerException = "[Conflicted] This trainer is assigned to one of the functionality and cannot be deleted.";
            conflictedMessage = innerException;

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };
            var responseBody = responseMessage.Content.ReadAsStringAsync();

            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            mockMessageHelper.Setup(x => x.CreateGenericConflictMessage(trainerControllerRoute)).Returns(conflictedMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));
            var dal = CreateTrainerDal();
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
            var trainer = Builder<Trainer>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            var dal = CreateTrainerDal();
            //Act
            await dal.PostAsync(trainer);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldCallResponseStatusCodeHelperPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            var trainer = Builder<Trainer>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);

            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateTrainerDal();
            //Act
            await dal.PostAsync(trainer);
            //Assert
            mockResponseStatusCodeHelper.Verify(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnTrainer_WhenCalled()
        {
            //Arrange
            var trainer = Builder<Trainer>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(trainer), null, "application/json")
            };

            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateTrainerDal();
            //Act
            var result = await dal.PostAsync(trainer);
            //Assert
            result.Should().BeEquivalentTo(trainer);
        }

        [Fact]
        public async Task PostAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadRequest;
            var trainer = Builder<Trainer>.CreateNew().Build();
            var innerException = "Duplicate values cannot be inserted. Please check all fields again before inserting the value.";

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };

            var responseBody = responseMessage.Content.ReadAsStringAsync();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));

            var dal = CreateTrainerDal();
            //Act
            Func<Task> result = async () => await dal.PostAsync(trainer);
            //Assert
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage(responseBody.ToString());
        }

        #endregion PostAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_ShouldCallHttpClientWrapperPutAsyncOnce_WhenCalled()
        {
            //Arrange
            var trainer = Builder<Trainer>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{trainer.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateTrainerDal();
            //Act
            await dal.PutAsync(trainer);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{trainer.Id}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldCallResponseStatusCodeHelperPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var statusCode = HttpStatusCode.BadRequest;
            var trainer = Builder<Trainer>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{trainer.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage));
            var dal = CreateTrainerDal();
            //Act
            await dal.PutAsync(trainer);

            //Assert
            mockResponseStatusCodeHelper.Verify(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnTrainer_WhenCalled()
        {
            //Arrange
            var trainer = Builder<Trainer>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(trainer), null, "application/json")
            };

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{trainer.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateTrainerDal();
            //Act
            var result = await dal.PutAsync(trainer);
            //Assert
            result.Should().BeEquivalentTo(trainer);
        }

        [Fact]
        public async Task PutAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadRequest;
            var trainer = Builder<Trainer>.CreateNew().Build();
            var innerException = "Duplicate values cannot be updated. Please check all fields again before inserting the value.";

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(innerException)
            };
            var responseBody = responseMessage.Content.ReadAsStringAsync();

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object
                            , $"{baseAddress}{trainerControllerRoute}/{trainer.Id}"
                            , It.IsAny<StringContent>()))
                            .ReturnsAsync(responseMessage);

            mockResponseStatusCodeHelper.Setup(x => x.FailedResponseFromAPI(responseMessage, conflictedMessage)).Throws(new HttpRequestException(responseBody.ToString(), null, responseMessage.StatusCode));
            var dal = CreateTrainerDal();
            //Act
            Func<Task> result = async () => await dal.PutAsync(trainer);
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
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}")).ReturnsAsync(responseMessage);

            var dal = CreateTrainerDal();
            //Act
            await dal.GetAllAsync();
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}"), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfTrainerFromClient_WhenSuccessful()
        {
            //Arrange
            var list = Builder<Trainer>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateTrainerDal();
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
            var list = Builder<Trainer>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateTrainerDal();
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
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateTrainerDal();
            //Act
            await dal.GetByIdAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTrainerFromResponseMessage_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var trainer = Builder<Trainer>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(trainer), null, "application/json");
            var dal = CreateTrainerDal();
            //Act
            var result = await dal.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(trainer);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            int id = 1;
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{trainerControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateTrainerDal();

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

        private TrainerDal CreateTrainerDal()
        {
            return new TrainerDal(mockHttpClient.Object, mockConfiguration.Object, mockHttpClientWrapper.Object, mockResponseStatusCodeHelper.Object, mockMessageHelper.Object);
        }
    }
}