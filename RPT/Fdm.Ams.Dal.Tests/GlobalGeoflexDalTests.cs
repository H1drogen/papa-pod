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
    public class GlobalGeoflexDalTests
    {
        private readonly string baseAddress;
        private readonly string globalGeoflexControllerRoute;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<HttpClient> mockHttpClient;
        private Mock<IHttpClientWrapper> mockHttpClientWrapper;
        private HttpResponseMessage responseMessage;

        public GlobalGeoflexDalTests()
        {
            mockHttpClient = new Mock<HttpClient>();
            mockConfiguration = new Mock<IConfiguration>();
            baseAddress = "https:fakehost";
            globalGeoflexControllerRoute = "/academy-hub/global-geoflex";
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
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateGlobalGeoflexDal();
            //Act
            await dal.DeleteAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            int id = 1;
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            var globalGeoflex = Builder<GlobalGeoflex>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(globalGeoflex), null, "application/json")
            };
            mockHttpClientWrapper.Setup(x => x.DeleteAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateGlobalGeoflexDal();
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
            var postGlobalGeoflexDto = Builder<PostGlobalGeoflexDto>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            var dal = CreateGlobalGeoflexDal();
            //Act
            await dal.PostAsync(postGlobalGeoflexDto);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}", It.IsAny<StringContent>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            var postGlobalGeoflexDto = Builder<PostGlobalGeoflexDto>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(postGlobalGeoflexDto), null, "application/json")
            };
            mockHttpClientWrapper.Setup(x => x.PostAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);
            var dal = CreateGlobalGeoflexDal();
            //Act
            try
            {
                await dal.PostAsync(postGlobalGeoflexDto);
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

        public async Task PutAsync_CallsHttpClientWrapperGetAsync_WithoutIdOnce_WhenCalled()
        {
            //Arrange
            var input = Builder<GlobalGeoflex>.CreateNew().Build();
            var returnedGeoFlex = Builder<GlobalGeoflex>.CreateNew().Build();
            var list = Builder<GlobalGeoflex>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            var getbyIdResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(returnedGeoFlex), null, "application/json")
            };

            var getAllResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json")
            };

            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{input.Id}")).ReturnsAsync(getbyIdResponseMessage);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}")).ReturnsAsync(getAllResponseMessage);
            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{returnedGeoFlex.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateGlobalGeoflexDal();
            //Act
            await dal.PutAsync(input);
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}"), Times.Once);
        }

        public async Task PutAsync_CallsHttpClientWrapperPutAsyncOnce_WhenCalled()
        {
            //Arrange
            var input = Builder<GlobalGeoflex>.CreateNew().Build();

            var list = Builder<GlobalGeoflex>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();

            mockHttpClientWrapper.Setup(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{input.Id}", It.IsAny<StringContent>())).ReturnsAsync(responseMessage);

            var dal = CreateGlobalGeoflexDal();
            //Act
            await dal.PutAsync(input);
            //Assert
            mockHttpClientWrapper.Verify(x => x.PutAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{input.Id}", It.IsAny<StringContent>()), Times.Once);
        }

        public async Task PutAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            var input = Builder<GlobalGeoflex>.CreateNew().Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(input), null, "application/json")
            };
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{input.Id}")).ReturnsAsync(responseMessage);
            var dal = CreateGlobalGeoflexDal();
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
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}")).ReturnsAsync(responseMessage);

            var dal = CreateGlobalGeoflexDal();
            //Act
            await dal.GetAllAsync();
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}"), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAnEnumerableOfGlobalGeoflexFromClient_WhenSuccessful()
        {
            //Arrange
            var list = Builder<GlobalGeoflex>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateGlobalGeoflexDal();
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
            var list = Builder<GlobalGeoflex>.CreateListOfSize(3).Build();
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(list), null, "application/json");
            var dal = CreateGlobalGeoflexDal();
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
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateGlobalGeoflexDal();
            //Act
            await dal.GetByIdAsync(id);
            //Assert
            mockHttpClientWrapper.Verify(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{id}"), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsGlobalGeoflexFromResponseMessage_WhenSuccessful()
        {
            //Arrange
            int id = 1;
            var globalGeoflex = Builder<GlobalGeoflex>.CreateNew().Build();

            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage();
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            responseMessage.Content = new StringContent(JsonConvert.SerializeObject(globalGeoflex), null, "application/json");
            var dal = CreateGlobalGeoflexDal();
            //Act
            var result = await dal.GetByIdAsync(id);
            //Assert
            result.Should().BeEquivalentTo(globalGeoflex);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsHttpRequestException_WhenANegativeResponseCodeIsProvided()
        {
            //Arrange
            int id = 1;
            var statusCode = System.Net.HttpStatusCode.BadGateway;
            mockConfiguration.Setup(x => x["ServiceDependencies:ApiBaseUrl"]).Returns(baseAddress);
            responseMessage = new HttpResponseMessage(statusCode);
            mockHttpClientWrapper.Setup(x => x.GetAsync(mockHttpClient.Object, $"{baseAddress}{globalGeoflexControllerRoute}/{id}")).ReturnsAsync(responseMessage);
            var dal = CreateGlobalGeoflexDal();

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

        private GlobalGeoflexDal CreateGlobalGeoflexDal()
        {
            return new GlobalGeoflexDal(mockHttpClient.Object, mockConfiguration.Object, mockHttpClientWrapper.Object);
        }
    }
}