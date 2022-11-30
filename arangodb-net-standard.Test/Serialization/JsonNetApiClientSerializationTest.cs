using ArangoDBNetStandard.CursorApi.Models;
using ArangoDBNetStandard.Serialization;
using ArangoDBNetStandard.TransactionApi.Models;
using ArangoDBNetStandardTest.Serialization.Models;
using Newtonsoft.Json;
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
                NullProperty = null,
                PropertyToCheckIfCamelCase = "myvalue",
                EnumToConvert = TestModel.Number.Two
            };

            var serialization = new JsonNetApiClientSerialization();

            // Perform serialize with camel case option
            byte[] jsonBytesWithCamelCase = serialization.Serialize(model,
                new ApiClientSerializationOptions(true, true, true));
            string jsonStringWithCamelCase = Encoding.UTF8.GetString(jsonBytesWithCamelCase);

            // Perform serialize without camel case option
            byte[] jsonBytesWithoutCamelCase = serialization.Serialize(model,
                new ApiClientSerializationOptions(false, true, true));
            string jsonStringWithoutCamelCase = Encoding.UTF8.GetString(jsonBytesWithoutCamelCase);

            // standard property with and without camelCase
            Assert.Contains("propertyToCheckIfCamelCase", jsonStringWithCamelCase);
            Assert.Contains("PropertyToCheckIfCamelCase", jsonStringWithoutCamelCase);

            // Null property should be ignored in both cases
            // (ignore case is important to make sure we don't miss the string 
            // if it is using different casing than we checked for)
            Assert.DoesNotContain(
                "nullProperty",
                jsonStringWithCamelCase,
                System.StringComparison.OrdinalIgnoreCase);

            Assert.DoesNotContain("nullProperty",
                jsonStringWithoutCamelCase,
                System.StringComparison.OrdinalIgnoreCase);

            // We expect enum conversion to string, as well as camelCase
            Assert.Contains("enumToConvert", jsonStringWithCamelCase);

            // We expect enum conversion to string, but not camelCase
            Assert.Contains("EnumToConvert", jsonStringWithoutCamelCase);
        }

        [Fact]
        public void Serialize_ShouldSucceed_WhenUsingDefaultOptions()
        {
            var model = new TestModel()
            {
                NullProperty = null,
                PropertyToCheckIfCamelCase = "myvalue",
                EnumToConvert = TestModel.Number.Two,
                PropertyWithDifferentJsonName = "aaaaaaa",
                MyStringDict = new Dictionary<string, object>()
                {
                    ["DictKeyNotCamelCasedAndNotIgnored"] = null,
                    ["moreParams"] = new TestModel()
                    {
                        PropertyToCheckIfCamelCase = "fneozirnzgiodedzf",
                        MyStringDict = new Dictionary<string, object>()
                        {
                            ["string1"] = "value1"
                        },
                        PropertyWithDifferentJsonName = "wow",
                        AnotherNullProperty = null,
                        EnumToConvert = TestModel.Number.One,
                        PropertyWithClassType = new InnerTestModel()
                        {
                            InnerTestModel_NullProperty = null,
                            InnerTestModel_PropertyToCheckIfCamelCase = "fjnzfiuzefnzeb"
                        }
                    }
                },
                PropertyWithClassType = new InnerTestModel()
                {
                    InnerTestModel_NullProperty = null,
                    InnerTestModel_PropertyToCheckIfCamelCase = "djenfiuzrfiubzeiu"
                }
            };
            var serialization = new JsonNetApiClientSerialization();

            // Perform serialize with default options 
            // i.e. camelCase: false, ignoreNull: true, stringEnum: false
            byte[] jsonBytes = serialization.Serialize(model, null);
            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Equal(
                "{\"PropertyToCheckIfCamelCase\":\"myvalue\",\"EnumToConvert\":2,\"nameFromJsonProperty\":\"aaaaaaa\",\"MyStringDict\":{\"DictKeyNotCamelCasedAndNotIgnored\":null,\"moreParams\":{\"PropertyToCheckIfCamelCase\":\"fneozirnzgiodedzf\",\"EnumToConvert\":1,\"nameFromJsonProperty\":\"wow\",\"MyStringDict\":{\"string1\":\"value1\"},\"PropertyWithClassType\":{\"InnerTestModel_PropertyToCheckIfCamelCase\":\"fjnzfiuzefnzeb\"}}},\"PropertyWithClassType\":{\"InnerTestModel_PropertyToCheckIfCamelCase\":\"djenfiuzrfiubzeiu\"}}",
                jsonString);

            // Explicited asserts

            Assert.Contains("PropertyToCheckIfCamelCase", jsonString);
            Assert.DoesNotContain(
                "NullProperty",
                jsonString,
                System.StringComparison.OrdinalIgnoreCase);
            Assert.Contains("EnumToConvert", jsonString);
            Assert.DoesNotContain("Two", jsonString);
            Assert.Contains("nameFromJsonProperty", jsonString);
            Assert.Contains("MyStringDict", jsonString);
            Assert.Contains("DictKeyNotCamelCasedAndNotIgnored", jsonString);
            Assert.Contains("moreParams", jsonString);
            Assert.Contains("string1", jsonString);
            Assert.DoesNotContain("PropertyWithDifferentJsonName", jsonString);
            Assert.DoesNotContain(
                "AnotherNullProperty",
                jsonString,
                System.StringComparison.OrdinalIgnoreCase);
            Assert.Contains("PropertyWithClassType", jsonString);
            Assert.Contains("InnerTestModel_PropertyToCheckIfCamelCase", jsonString);
        }

        [Fact]
        public void DefaultOptions_ShouldAllowValuesToBeModifiedInEachInstances()
        {
            var serialization1 = new JsonNetApiClientSerialization();
            var serialization2 = new JsonNetApiClientSerialization();

            AssertDefaultOptions(serialization1.DefaultOptions);

            serialization1.DefaultOptions.UseCamelCasePropertyNames = true;
            serialization1.DefaultOptions.IgnoreNullValues = false;
            serialization1.DefaultOptions.UseStringEnumConversion = true;

            Assert.True(serialization1.DefaultOptions.UseCamelCasePropertyNames);
            Assert.False(serialization1.DefaultOptions.IgnoreNullValues);
            Assert.True(serialization1.DefaultOptions.UseStringEnumConversion);

            // Ensure options for each instances are independent

            AssertDefaultOptions(serialization2.DefaultOptions);
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

            byte[] jsonBytes = serialization.Serialize(body, new ApiClientSerializationOptions(
                useCamelCasePropertyNames: true,
                ignoreNullValues: true));

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("DontCamelCaseMe", jsonString);
            Assert.Contains("DontCamelCaseKey", jsonString);
            Assert.DoesNotContain("dontCamelCaseMe", jsonString);
            Assert.DoesNotContain("dontCamelCaseKey", jsonString);
        }

        [Fact]
        public void Serialize_ShouldCamelCaseBindVars_WhenSerializingPostCursorBody()
        {
            var body = new PostCursorBody
            {
                BindVars = new Dictionary<string, object>
                {
                    ["CamelCaseKey"] = new { CamelCaseMe = true }
                }
            };
            var serialization = new JsonNetApiClientSerialization();

            byte[] jsonBytes = serialization.Serialize(body, new ApiClientSerializationOptions(
                useCamelCasePropertyNames: true, 
                ignoreNullValues: true));

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("CamelCaseKey", jsonString);
            Assert.DoesNotContain("camelCaseKey", jsonString);
            Assert.Contains("camelCaseMe", jsonString);
            Assert.DoesNotContain("CamelCaseMe", jsonString);
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

            byte[] jsonBytes = serialization.Serialize(body, new ApiClientSerializationOptions(
                 useCamelCasePropertyNames: true,
                 ignoreNullValues: true));

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("DontCamelCaseMe", jsonString);
            Assert.Contains("DontCamelCaseKey", jsonString);
            Assert.DoesNotContain("dontCamelCaseMe", jsonString);
            Assert.DoesNotContain("dontCamelCaseKey", jsonString);
        }


        [Fact]
        public void Serialize_ShouldCamelCaseParams_WhenSerializingPostTransactionBody()
        {
            var body = new PostTransactionBody
            {
                Params = new Dictionary<string, object>
                {
                    ["CamelCaseKey"] = new { CamelCaseMe = true }
                }
            };

            var serialization = new JsonNetApiClientSerialization();

            byte[] jsonBytes = serialization.Serialize(body, new ApiClientSerializationOptions(
                 useCamelCasePropertyNames: true,
                 ignoreNullValues: true,
                 applySerializationOptionsToObjectValuesInDictionaries: true));

            string jsonString = Encoding.UTF8.GetString(jsonBytes);

            Assert.Contains("CamelCaseKey", jsonString);
            Assert.DoesNotContain("camelCaseKey", jsonString);
            Assert.Contains("camelCaseMe", jsonString);
            Assert.DoesNotContain("CamelCaseMe", jsonString);
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
                "{\"propertyToCheckIfCamelCase\":\"myvalue\",\"NullProperty\":\"something\"}");

            var stream = new MemoryStream(jsonBytes);

            var serialization = new JsonNetApiClientSerialization();

            TestModel model = serialization.DeserializeFromStream<TestModel>(stream);

            Assert.Equal("myvalue", model.PropertyToCheckIfCamelCase);
            Assert.Equal("something", model.NullProperty);
        }

        private void AssertDefaultOptions(ApiClientSerializationOptions options)
        {
            Assert.False(options.UseCamelCasePropertyNames);
            Assert.True(options.IgnoreNullValues);
            Assert.False(options.UseStringEnumConversion);
        }
    }
}
