using ArangoDBNetStandard.CursorApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.TransactionApi.Models;
using ArangoDBNetStandardTest.Serialization.Models;
using System.Collections.Generic;
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

            byte[] jsonBytes = serialization.Serialize(model, new ApiClientSerializationOptions(true, true));

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("propertyToCamelCase", jsonString);
            Assert.DoesNotContain("nullPropertyToIgnore", jsonString);
        }

        [Fact]
        public void Serialize_ShouldNotCamelCaseBindVars_WhenSerializingPostCursorBody()
        {
            var body = new PostCursorBody
            {
                BindVars = new Dictionary<string, object>
                {
                    ["DontCamelCaseKey"] = new { DontCamelCaseMe = true }
                }
            };
            var serialization = new JsonNetApiClientSerialization();

            byte[] jsonBytes = serialization.Serialize(body, new ApiClientSerializationOptions(true, true));

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("DontCamelCaseMe", jsonString);
            Assert.Contains("DontCamelCaseKey", jsonString);
            Assert.DoesNotContain("dontCamelCaseMe", jsonString);
            Assert.DoesNotContain("dontCamelCaseKey", jsonString);
        }

        [Fact]
        public void Serialize_ShouldNotCamelCaseParams_WhenSerializingPostTransactionBody()
        {
            var body = new PostTransactionBody
            {
                Params = new Dictionary<string, object>
                {
                    ["DontCamelCaseKey"] = new { DontCamelCaseMe = true }
                }
            };

            var serialization = new JsonNetApiClientSerialization();

            byte[] jsonBytes = serialization.Serialize(body, new ApiClientSerializationOptions(true, true));

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("DontCamelCaseMe", jsonString);
            Assert.Contains("DontCamelCaseKey", jsonString);
            Assert.DoesNotContain("dontCamelCaseMe", jsonString);
            Assert.DoesNotContain("dontCamelCaseKey", jsonString);
        }


        [Fact]
        public void Serialize_ShouldNotIgnoreNull_WhenSerializingPostCursorBody()
        {
            var body = new PostCursorBody
            {
                BindVars = new Dictionary<string, object>
                {
                    ["DontCamelCaseKey"] = null
                }
            };
            var serialization = new JsonNetApiClientSerialization();

            byte[] jsonBytes = serialization.Serialize(body, new ApiClientSerializationOptions(true, true));

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("DontCamelCaseKey", jsonString);
        }


        [Fact]
        public void Serialize_ShouldNotIgnoreNull_WhenSerializingPostTransactionBody()
        {
            var body = new PostTransactionBody
            {
                Params = new Dictionary<string, object>
                {
                    ["DontCamelCaseKey"] = null
                }
            };

            var serialization = new JsonNetApiClientSerialization();

            byte[] jsonBytes = serialization.Serialize(body, new ApiClientSerializationOptions(true, true));

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("DontCamelCaseKey", jsonString);
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
