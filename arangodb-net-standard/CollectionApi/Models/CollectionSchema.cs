namespace ArangoDBNetStandard.CollectionApi.Models
{
    /// <summary>
    /// Specifies the collection level schema for documents.
    /// </summary>
    public class CollectionSchema
    {
        /// <summary>
        /// Defines the JSON Schema description for schema validation.
        /// See https://www.arangodb.com/docs/3.9/data-modeling-documents-schema-validation.html
        /// </summary>
        public object Rule { get; set; }

        /// <summary>
        /// Controls when the validation will be applied/triggered.
        /// For possible values <see cref="CollectionSchemaLevel"/>
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// The message that is used when validation fails.
        /// If the schema validation for a document fails, then a generic 
        /// error is raised. The schema validation cannot pin-point which
        /// part of a rule made it fail. You may customize the error message 
        /// via the message attribute to provide a summary of what is expected
        /// or point out common mistakes.
        /// </summary>
        public string Message { get; set; }
    }
}
