using ArangoDBNetStandard;
using ArangoDBNetStandard.AnalyzerApi;
using ArangoDBNetStandard.AnalyzerApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDBNetStandardTest.AnalyzerApi
{
    public class AnalyzerApiClientTest : IClassFixture<AnalyzerApiClientTestFixture>, IAsyncLifetime
    {
        private IAnalyzerApiClient _analyzerApi;
        private ArangoDBClient _adb;

        public AnalyzerApiClientTest(AnalyzerApiClientTestFixture fixture)
        {
            _adb = fixture.ArangoDBClient;
            _analyzerApi = _adb.Analyzer;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        [Trait("Feature", "Analyzer")]
        public async Task GetAllAnalyzersAsync_ShouldSucceed()
        {
            var res = await _analyzerApi.GetAllAnalyzersAsync();
            Assert.Equal(HttpStatusCode.OK, res.Code);
            Assert.False(res.Error);
        }

        [Fact]
        [Trait("Feature", "Analyzer")]
        public async Task PostAnalyzerAsync_ShouldSucceed()
        {
            var res = await _analyzerApi.PostAnalyzerAsync(
                new Analyzer()
                {
                    Name = "text_sc",
                    Type = "identity",
                    Properties = new AnalyzerProperties()
                    {
                        Accent = false,
                        Case = "lower",
                        Locale = "sc",
                        Stemming = false,
                        StopWords = new List<string>()
                    },
                    Features = new List<string>() 
                    { 
                        "frequency", 
                        "position", 
                        "norm" 
                    }
                }
                );
            Assert.NotNull(res);
            Assert.NotNull(res.Name);
        }

        [Fact]
        [Trait("Feature", "Analyzer")]
        public async Task PostAnalyzerAsync_ShouldThrow_WhenAnalyzerIsInvalid()
        {
            try
            {
                //This call should fail because...
                var res = await _analyzerApi.PostAnalyzerAsync(
                    new Analyzer()
                    {
                        Name = "text_sc",
                        Type = "collection", //...invalid analyzer type
                    Properties = new AnalyzerProperties()
                        {
                            Accent = false,
                            Case = "lower",
                            Locale = "sc",
                            Stemming = false,
                            StopWords = new List<string>()
                        },
                        Features = new List<string>()
                        {
                        "frequency",
                        "position",
                        "norm"
                        }
                    }
                    );
            }
            catch (ApiErrorException ex)
            {
                Assert.NotNull(ex);
                Assert.NotNull(ex.ApiError);
                Assert.True(ex.ApiError.Error);
            }
        }

        [Fact]
        [Trait("Feature", "Analyzer")]
        public async Task GetAnalyzerAsync_ShouldSucceed()
        {
            var allRes = await _analyzerApi.GetAllAnalyzersAsync();
            var firstName = allRes.Result[0].Name;
            var res = await _analyzerApi.GetAnalyzerAsync(firstName);
            Assert.Equal(HttpStatusCode.OK, res.Code);
            Assert.False(res.Error);
        }

        [Fact]
        [Trait("Feature", "Analyzer")]
        public async Task DeleteAnalyzerAsync_ShouldSucceed()
        {
            var name = "text_mu";
            var createRes = await _analyzerApi.PostAnalyzerAsync(
                new Analyzer()
                {
                    Name = name,
                    Type = "identity",
                    Properties = new AnalyzerProperties()
                    {
                        Accent = false,
                        Case = "lower",
                        Locale = "mu",
                        Stemming = false,
                        StopWords = new List<string>()
                    },
                    Features = new List<string>()
                    {
                        "frequency",
                        "position",
                        "norm"
                    }
                }
                );
            var res = await _analyzerApi.DeleteAnalyzerAsync(name);
            Assert.Equal(HttpStatusCode.OK, res.Code);
            Assert.False(res.Error);
        }
    }
}