using Fdm.Ams.Common;
using Fdm.Ams.Models;
using Fdm.Ams.Models.Post;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Fdm.Ams.Dal.Tests
{
    public class ConsultantModuleDalTest
    {
        private readonly string baseAddress;
        private readonly string consultantModuleControllerRoute;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<HttpClient> mockHttpClient;
        private Mock<IHttpClientWrapper> mockHttpClientWrapper;
        private HttpResponseMessage responseMessage;

        public ConsultantModuleDalTest()
        {
            mockHttpClient = new Mock<HttpClient>();
            mockConfiguration = new Mock<IConfiguration>();
            baseAddress = "https:fakehost";
            consultantModuleControllerRoute = "/academy-hub/consultant-modules";
            mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
        }

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_CallsHttpClientWrapperDeleteAsyncOnce_WhenCalled()
        {
            //Arrange
            int id = 1;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateConsultantModuleDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            int id = 1;
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            var consultantModule = Builder<ConsultantModule>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(consultantModule), null, "application/json")
            };
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateConsultantModuleDal();
            //Act
            try
            {
                await dal.DeleteAsync(id);
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

        #endregion DeleteAsync

        #region PostAsync

        [Fact]
        public async Task PostAsync_CallsHttpClientWrapperPostAsyncOnce_WhenCalled()
        {
            //Arrange
            var postConsultantModuleDto = Builder<PostConsultantModuleDto>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            var dal = CreateConsultantModuleDal();
            //Act
            await dal.PostAsync(postConsultantModuleDto);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            var postConsultantModuleDto = Builder<PostConsultantModuleDto>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(postConsultantModuleDto), null, "application/json")
            };
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            var dal = CreateConsultantModuleDal();
            //Act
            try
            {
                await dal.PostAsync(postConsultantModuleDto);
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

        #endregion PostAsync

        #region PutAsync

        [Fact]
        public async Task PutAsync_CallsHttpClientWrapperPutAsyncOnce_WhenCalled()
        {
            //Arrange
            var input = Builder<ConsultantModule>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}/{input.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateConsultantModuleDal();
            //Act
            await dal.PutAsync(input);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}/{input.Id}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            var input = Builder<ConsultantModule>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(input), null, "application/json")
            };
            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object
                 , $"{baseAddress}{consultantModuleControllerRoute}/{input.Id}"
                 , It.IsAny<StringContent>()))
                 .ReturnsAsync(responseMessage);
            var dal = CreateConsultantModuleDal();
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
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}")).ReturnsAsync(responseMessage);

            var dal = CreateConsultantModuleDal();
            //Act
            await dal.GetAllAsync();
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}"), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfConsultantModuleFromClient_WhenSuccessful()
        {
            //Arrange
            var list = Builder<ConsultantModule>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateConsultantModuleDal();
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
            var list = Builder<ConsultantModule>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateConsultantModuleDal();
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
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateConsultantModuleDal();
            //Act
            await dal.GetByIdAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsConsultantModuleFromResponseMessage_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var consultantModule = Builder<ConsultantModule>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(consultantModule), null, "application/json");
            var dal = CreateConsultantModuleDal();
            //Act
            var result = await dal.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(consultantModule);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            int id = 1;
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{consultantModuleControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateConsultantModuleDal();

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

        private ConsultantModuleDal CreateConsultantModuleDal()
        {
            return new ConsultantModuleDal(mockHttpClient.Object, mockConfiguration.Object, mockHttpClientWrapper.Object);
        }
    }
}