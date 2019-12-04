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
        public void Serialize_ShouldSucceed()
        {
            var model = new TestModel()
            {
                NullPropertyToIgnore = null,
                PropertyToCamelCase = "myvalue"
            };

            var serialization = new JsonNetApiClientSerialization();

            byte[] jsonBytes = serialization.Serialize(model, true, true);

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("propertyToCamelCase", jsonString);
            Assert.DoesNotContain("nullPropertyToIgnore", jsonString);
        }

        [Fact]
        public void DeserializeFromStream_ShouldSucceed()
        {
            // Deserializing should match both "camelCase" and "CamelCase"

            byte[] jsonBytes = Encoding.UTF8.GetBytes(
                "{\"propertyToCamelCase\":\"myvalue\",\"NullPropertyToIgnore\":\"something\"}");

            var stream = new MemoryStream(jsonBytes);

            var serialization = new JsonNetApiClientSerialization();

            TestModel model = serialization.DeserializeFromStream<TestModel>(stream);

            Assert.Equal("myvalue", model.PropertyToCamelCase);
            Assert.Equal("something", model.NullPropertyToIgnore);
        }
    }
}
