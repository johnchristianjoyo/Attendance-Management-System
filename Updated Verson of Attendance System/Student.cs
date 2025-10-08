using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem2.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full Name")]
        [BsonElement("fullName")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "Section is required")]
        [BsonElement("section")]
        public string Section { get; set; } = "";

        [Required(ErrorMessage = "Student ID is required")]
        [Display(Name = "Student ID")]
        [BsonElement("studentId")]
        public string StudentId { get; set; } = "";

        [BsonElement("dateAdded")]
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
