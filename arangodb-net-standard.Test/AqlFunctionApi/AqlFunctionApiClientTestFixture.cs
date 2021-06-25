using ArangoDBNetStandard.AqlFunctionApi;
using System.Threading.Tasks;

namespace ArangoDBNetStandardTest.AqlFunctionApi
{
    /// <summary>
    /// Provides per-test-class fixture data for <see cref="AqlFunctionApiClientTest"/>.
    /// </summary>
    public class AqlFunctionApiClientTestFixture : ApiClientTestFixtureBase
    {
        public AqlFunctionApiClient AqlFunctionClient { get; set; }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync().ConfigureAwait(false);

            string dbName = nameof(AqlFunctionApiClientTestFixture);

            await CreateDatabase(dbName).ConfigureAwait(false);

            AqlFunctionClient = GetArangoDBClient(dbName).AqlFunction;
        }
    }
}
