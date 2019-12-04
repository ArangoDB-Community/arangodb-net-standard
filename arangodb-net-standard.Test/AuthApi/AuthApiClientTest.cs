using ArangoDBNetStandard;
using ArangoDBNetStandard.AuthApi;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest.AuthApi
{
    public class AuthApiClientTest: IClassFixture<AuthApiClientTestFixture>
    {
        private AuthApiClientTestFixture _fixture;

        public AuthApiClientTest(AuthApiClientTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetJwtToken_ShouldSucceed()
        {
            JwtTokenResponse response = await _fixture.ArangoDBClient.Auth.GetJwtToken(
                new JwtTokenRequestBody
                {
                    Username = _fixture.Username,
                    Password = _fixture.Password
                });
            Assert.NotNull(response.Jwt);
        }

        [Fact]
        public async Task GetJwtToken_ShouldThrow_WhenWrongCredentialsUsed()
        {
            var ex = await Assert.ThrowsAsync<ApiErrorException>(async () =>
                await _fixture.ArangoDBClient.Auth.GetJwtToken(
                    new JwtTokenRequestBody
                    {
                        Username = _fixture.Username,
                        Password = "wrongpw"
                    }));

            Assert.NotNull(ex.ApiError);
            Assert.True(ex.ApiError.Error);
            Assert.Equal(HttpStatusCode.Unauthorized, ex.ApiError.Code);
            Assert.Equal(401, ex.ApiError.ErrorNum);
            Assert.NotNull(ex.ApiError.ErrorMessage);
        }
    }
}
