using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AttendanceSystem2.Models
{
    public class Attendance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("studentId")]
        public string StudentId { get; set; } = "";

        [BsonElement("fullName")]
        public string FullName { get; set; } = "";

        [BsonElement("section")]
        public string Section { get; set; } = "";

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = "";

        [BsonElement("markedAt")]
        public DateTime MarkedAt { get; set; } = DateTime.Now;
    }
}
