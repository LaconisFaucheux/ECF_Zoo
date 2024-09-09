using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_Arcadia.Models
{
    public class HabitatStats
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int HabitatId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MiniSlug { get; set; } = string.Empty;
        public uint NbClics { get; set; }
    }
}
