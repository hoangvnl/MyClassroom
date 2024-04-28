using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyConfigurationServer.gRPC.Contracts
{
    public class ClassroomConfiguration
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public Guid ClassroomId { get; set; }
        public string Color { get; set; } = null!;

    }
}
