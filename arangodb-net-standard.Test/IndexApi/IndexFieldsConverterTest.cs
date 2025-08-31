using System;
using System.Collections.Generic;
using System.Linq;
using ArangoDBNetStandard.IndexApi.Converters;
using ArangoDBNetStandard.IndexApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace ArangoDBNetStandardTest.IndexApi
{
    public class IndexFieldsConverterTest
    {
        private readonly JsonSerializerSettings _settings;

        public IndexFieldsConverterTest()
        {
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Include
            };
            _settings.Converters.Add(new IndexFieldsConverter());
        }

        [Fact]
        public void ReadJson_ShouldDeserializeStringArray_WhenJsonIsSimpleStringArray()
        {
            // Arrange
            var json = """
            {
                "fields": ["field1", "field2", "field3"]
            }
            """;

            // Act
            var result = JsonConvert.DeserializeObject<TestIndexResponse>(json, _settings);

            // Assert
            Assert.NotNull(result.Fields);
            Assert.Equal(3, result.Fields.Count());
            Assert.Contains("field1", result.Fields);
            Assert.Contains("field2", result.Fields);
            Assert.Contains("field3", result.Fields);
        }

        [Fact]
        public void ReadJson_ShouldExtractNameFields_WhenJsonIsObjectArrayWithNameProperty()
        {
            // Arrange
            var json = """
            {
                "fields": [
                    {"name": "field1", "type": "string"},
                    {"name": "field2", "type": "number"},
                    {"name": "field3", "type": "boolean"}
                ]
            }
            """;

            // Act
            var result = JsonConvert.DeserializeObject<TestIndexResponse>(json, _settings);

            // Assert
            Assert.NotNull(result.Fields);
            Assert.Equal(3, result.Fields.Count());
            Assert.Contains("field1", result.Fields);
            Assert.Contains("field2", result.Fields);
            Assert.Contains("field3", result.Fields);
        }

        [Fact]
        public void ReadJson_ShouldFilterOutNullNames_WhenObjectArrayHasNullNameValues()
        {
            // Arrange
            var json = """
            {
                "fields": [
                    {"name": "field1", "type": "string"},
                    {"name": null, "type": "number"},
                    {"name": "field3", "type": "boolean"}
                ]
            }
            """;

            // Act
            var result = JsonConvert.DeserializeObject<TestIndexResponse>(json, _settings);

            // Assert
            Assert.NotNull(result.Fields);
            Assert.Equal(2, result.Fields.Count());
            Assert.Contains("field1", result.Fields);
            Assert.Contains("field3", result.Fields);
            Assert.DoesNotContain(null, result.Fields);
        }

        [Fact]
        public void ReadJson_ShouldHandleEmptyArray()
        {
            // Arrange
            var json = """
            {
                "fields": []
            }
            """;

            // Act
            var result = JsonConvert.DeserializeObject<TestIndexResponse>(json, _settings);

            // Assert
            Assert.NotNull(result.Fields);
            Assert.Empty(result.Fields);
        }

        [Fact]
        public void ReadJson_ShouldThrowJsonSerializationException_WhenTokenIsNotArray()
        {
            // Arrange
            var json = """
            {
                "fields": "not an array"
            }
            """;

            // Act & Assert
            Assert.Throws<JsonSerializationException>(() => 
                JsonConvert.DeserializeObject<TestIndexResponse>(json, _settings));
        }

        [Fact]
        public void ReadJson_ShouldReturnNull_WhenTokenIsNull()
        {
            // Arrange
            var json = """
            {
                "fields": null
            }
            """;

            // Act
            var result = JsonConvert.DeserializeObject<TestIndexResponse>(json, _settings);

            // Assert
            Assert.Null(result.Fields);
        }

        [Fact]
        public void ReadJson_ShouldHandleMixedObjectAndStringArray_WhenFirstElementIsNotObject()
        {
            // Arrange - this tests the case where we have an array but the first element is not an object
            var json = """
            {
                "fields": ["field1", "field2"]
            }
            """;

            // Act
            var result = JsonConvert.DeserializeObject<TestIndexResponse>(json, _settings);

            // Assert
            Assert.NotNull(result.Fields);
            Assert.Equal(2, result.Fields.Count());
            Assert.Contains("field1", result.Fields);
            Assert.Contains("field2", result.Fields);
        }

        [Fact]
        public void WriteJson_ShouldSerializeToStringArray()
        {
            // Arrange
            var indexResponse = new TestIndexResponse
            {
                Fields = new[] { "field1", "field2", "field3" }
            };

            // Act
            var json = JsonConvert.SerializeObject(indexResponse, _settings);

            // Assert
            Assert.Contains("\"fields\":[\"field1\",\"field2\",\"field3\"]", json);
        }

        [Fact]
        public void WriteJson_ShouldSerializeNullFields()
        {
            // Arrange
            var indexResponse = new TestIndexResponse
            {
                Fields = null
            };

            // Act
            var json = JsonConvert.SerializeObject(indexResponse, _settings);

            // Assert
            Assert.Contains("\"fields\":null", json);
        }

        [Fact]
        public void WriteJson_ShouldSerializeEmptyFields()
        {
            // Arrange
            var indexResponse = new TestIndexResponse
            {
                Fields = new string[0]
            };

            // Act
            var json = JsonConvert.SerializeObject(indexResponse, _settings);

            // Assert
            Assert.Contains("\"fields\":[]", json);
        }

        [Fact]
        public void RoundTrip_ShouldPreserveStringArrayData()
        {
            // Arrange
            var originalFields = new List<string> { "field1", "field2", "field3" };
            var indexResponse = new TestIndexResponse { Fields = originalFields };

            // Act
            var json = JsonConvert.SerializeObject(indexResponse, _settings);
            var deserializedResponse = JsonConvert.DeserializeObject<TestIndexResponse>(json, _settings);

            // Assert
            Assert.NotNull(deserializedResponse.Fields);
            Assert.Equal(originalFields.Count, deserializedResponse.Fields.Count());
            Assert.True(originalFields.SequenceEqual(deserializedResponse.Fields));
        }

        // Helper class for testing
        private class TestIndexResponse
        {
            [JsonConverter(typeof(IndexFieldsConverter))]
            public IEnumerable<string> Fields { get; set; }
        }
    }
}
