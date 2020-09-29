using System.IO;

namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// The Api client serilization abastract class.
    /// Used as a base to implement custom serilizations.
    /// </summary>
    public abstract class ApiClientSerialization : IApiClientSerialization
    {
        /// <summary>
        /// Serializes the specified object to a sequence of bytes,
        /// override this method to implement the custom serializer action the given object.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="item">The object to serialize.</param>
        /// <param name="options">The serialization content type.</param>
        /// <returns></returns>
        protected abstract byte[] SerializeItem<T>(T item, ApiClientSerializationOptions options);

        /// <summary>
        /// Gets the serizilzation options for the given content type.
        /// Override this method in order to implement custom serialization
        /// options, otherwise these defaults options will be
        /// applied.
        /// </summary>
        /// <param name="contentType">The content type.</param>
        /// <returns></returns>
        protected virtual ApiClientSerializationOptions GetSerializationOptions(ApiClientSerializationContentType contentType)
        {
            switch(contentType){
                case ApiClientSerializationContentType.PostCursor:
                case ApiClientSerializationContentType.PostAqlFunction:
                case ApiClientSerializationContentType.PostCollection:
                case ApiClientSerializationContentType.PostGraph:
                case ApiClientSerializationContentType.PostEdgeDefinition:
                case ApiClientSerializationContentType.PutEdgeDefinition:
                case ApiClientSerializationContentType.PatchEdge:
                case ApiClientSerializationContentType.PutVertex:
                case ApiClientSerializationContentType.PostTransaction:
                case ApiClientSerializationContentType.PostUser:
                case ApiClientSerializationContentType.PutUser:
                case ApiClientSerializationContentType.PatchUser:
                case ApiClientSerializationContentType.PutCollectionAccessLevel:
                case ApiClientSerializationContentType.PutDatabaseAccessLevel:
                case ApiClientSerializationContentType.PostDatabase:
                case ApiClientSerializationContentType.PutCollection:
                case ApiClientSerializationContentType.PostVertexCollection:
                    return new ApiClientSerializationOptions(true, true);

                case ApiClientSerializationContentType.PostDocument:
                case ApiClientSerializationContentType.PostDocuments:
                case ApiClientSerializationContentType.PutDocument:
                case ApiClientSerializationContentType.PutDocuments:
                case ApiClientSerializationContentType.DeleteDocuments:
                case ApiClientSerializationContentType.PatchDocuments:
                case ApiClientSerializationContentType.PatchDocument:
                case ApiClientSerializationContentType.PostVertex:
                case ApiClientSerializationContentType.PostEdge:
                case ApiClientSerializationContentType.PatchVertex:
                case ApiClientSerializationContentType.PutEdge:
                    return new ApiClientSerializationOptions(false, false);

                case ApiClientSerializationContentType.GetJwtToken:
                case ApiClientSerializationContentType.RenameCollection:
                    return new ApiClientSerializationOptions(true, false);

                case ApiClientSerializationContentType.GetDocuments:
                    return new ApiClientSerializationOptions(false, true);
            }

            throw new System.Exception("No options found for the specific serialization content type");
        }

        /// <summary>
        /// Deserializes the data structure contained by the specified stream
        /// into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="stream">The stream containing the JSON structure to deserialize.</param>
        /// <returns></returns>
        public abstract T DeserializeFromStream<T>(Stream stream);

        /// <summary>
        /// Serializes the specified object to a sequence of bytes,
        /// following the provided rules for camel case property name and null value handling.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="item">The object to serialize.</param>
        /// <param name="contentType">The serialization content type.</param>
        /// <returns></returns>
        public byte[] Serialize<T>(T item, ApiClientSerializationContentType contentType)
        {
            var options = GetSerializationOptions(contentType);
            return SerializeItem(item, options);
        }
    }
}
