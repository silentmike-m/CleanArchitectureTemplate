namespace SilentMike.IntegrationTests
{
    using System;
    using System.Net;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SilentMike.Infrastructure.HealthCheck.Models;
    using SilentMike.WebApi;
    using SilentMike.WebApi.Models;

#if DEBUG
    [TestClass]
    public sealed class StartupTests : IDisposable
    {
        private readonly WebApplicationFactory<Startup> factory;

        public StartupTests()
        {
            this.factory = new WebApplicationFactory<Startup>();
        }

        public void Dispose()
        {
            this.factory.Dispose();
        }

        [TestMethod]
        public async Task ShouldReturnHealthCheck()
        {
            //GIVEN
            var client = this.factory.CreateClient();
            const string url = "/health";

            //WHEN
            var response = await client.GetAsync(url);

            //THEN
            response.StatusCode.Should()
                .Be(HttpStatusCode.OK)
                ;
            response.Content.Headers.ContentType?.ToString()
                .Should()
                .Be("application/json")
                ;

            var baseResponse = await response.Content.ReadFromJsonAsync<BaseResponse<MainHealthCheck>>();
            baseResponse.Should()
                .NotBeNull()
                ;
            baseResponse!.Code.Should()
                .Be("OK")
                ;
            baseResponse.Error.Should()
                .BeNull()
                ;
            baseResponse.Response.Should()
                .NotBeNull()
                ;
        }

        [TestMethod]
        public async Task ShouldReturnSwagger()
        {
            //GIVEN
            var client = this.factory.CreateClient();

            //WHEN
            var response = await client.GetAsync("/swagger/v1/swagger.json");

            //THEN
            response.StatusCode.Should()
                .Be(HttpStatusCode.OK)
                ;

            response.Content.Headers.ContentType?.ToString()
                .Should()
                .Be("application/json; charset=utf-8")
                ;
        }
    }
#endif
}
