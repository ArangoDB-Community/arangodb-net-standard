using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandardTest.Serialization.Models;
using System.IO;
using System.Text;
using Xunit;

namespace ArangoDBNetStandardTest.Serialization
{
    public class JsonNetApiClientSerializationTest
    {
        [Fact]
        public void SerializeToJson_ShouldSucceed()
        {
            var model = new TestModel()
            {
                NullPropertyToIgnore = null,
                PropertyToCamelCase = "myvalue"
            };

            var serialization = new JsonNetApiClientSerialization();

            byte[] jsonBytes = serialization.SerializeToJson(model, true, true);

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("propertyToCamelCase", jsonString);
            Assert.DoesNotContain("nullPropertyToIgnore", jsonString);
        }

        [Fact]
        public void DeserializeJsonFromStream_ShouldSucceed()
        {
            // Deserializing should match both "camelCase" and "CamelCase"

            byte[] jsonBytes = Encoding.UTF8.GetBytes(
                "{\"propertyToCamelCase\":\"myvalue\",\"NullPropertyToIgnore\":\"something\"}");

            var stream = new MemoryStream(jsonBytes);

            var serialization = new JsonNetApiClientSerialization();

            TestModel model = serialization.DeserializeJsonFromStream<TestModel>(stream);

            Assert.Equal("myvalue", model.PropertyToCamelCase);
            Assert.Equal("something", model.NullPropertyToIgnore);
        }
    }
}
