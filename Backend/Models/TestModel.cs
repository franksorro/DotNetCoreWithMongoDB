using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotNetCoreWithMongoDB.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TestModel
    {
        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [BsonElement("Element")]
        public string Label { get; set; }
    }
}
