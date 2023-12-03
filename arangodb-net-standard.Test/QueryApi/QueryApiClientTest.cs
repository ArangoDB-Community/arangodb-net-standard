using ArangoDBNetStandard.QueryApi;
using ArangoDBNetStandard.QueryApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArangoDBNetStandard.CursorApi;
using Xunit;
using ArangoDBNetStandard.CursorApi.Models;

namespace ArangoDBNetStandardTest.QueryApi
{
    /// <summary>
    /// Class used for testing query API.
    /// </summary>
    public class QueryApiClientTest : IClassFixture<QueryApiClientTestFixture>
    {
        private readonly IQueryApiClient _queryApi;
        private readonly ICursorApiClient _cursorApi;
        private readonly string _testCollection;

        public QueryApiClientTest(QueryApiClientTestFixture fixture)
        {
            _queryApi = fixture.ArangoDBClient.Query;
            _cursorApi = fixture.ArangoDBClient.Cursor;
            _testCollection = fixture.TestCollection;

            var request = new PostQueryBody { Query = "FOR doc IN TestCollection REMOVE doc IN TestCollection" };
            _ = Task.Run(() => _queryApi.PostExecuteNonQueryAsync(request)).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task InsertQuery_ShouldSucceed()
        {
            var request = new PostQueryBody
            { 
                Query = "FOR Name IN [\"Jon\",\"Snow\"] INSERT {Name: Name} INTO @@testCollection",
                BindVars = new Dictionary<string, object> { ["@testCollection"] = _testCollection } 
            };

            var response = await _queryApi.PostExecuteNonQueryAsync(request);

            Assert.Equal(2, response.Extra.Stats.WritesExecuted);
            Assert.Equal(201, response.Code);
            Assert.False(response.Error);
        }

        [Fact]
        public async Task UpdateQuery_ShouldSucceed()
        {
            var request = new PostQueryBody
            {
                Query = "INSERT {Name: 'Test Name'} INTO @@testCollection",
                BindVars = new Dictionary<string, object> { ["@testCollection"] = _testCollection }
            };

            _ = await _queryApi.PostExecuteNonQueryAsync(request);

            request = new PostQueryBody
            {
                Query = "FOR doc IN @@testCollection UPDATE doc WITH {Name: 'Updated Name'} IN @@testCollection",
                BindVars = new Dictionary<string, object> { ["@testCollection"] = _testCollection }
            };

            var response = await _queryApi.PostExecuteNonQueryAsync(request);

            var responseQuery = await _cursorApi.PostCursorAsync<Doc>(new PostCursorBody
            {
                Query = "FOR doc IN @@testCollection RETURN doc",
                BindVars = new Dictionary<string, object> { ["@testCollection"] = _testCollection }
            });

            Assert.Equal(1, response.Extra.Stats.WritesExecuted);
            Assert.Equal(201, response.Code);
            Assert.False(response.Error);
            Assert.Equal("Updated Name", responseQuery.Result.FirstOrDefault().Name);
        }

        public class Doc
        {
            public string Name { get; set; }
        }
    }
}
