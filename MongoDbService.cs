using MongoDB.Driver;
using AttendanceSystem2.Models;

namespace AttendanceSystem2.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Student> Students => _database.GetCollection<Student>("students");

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await Students.Find(_ => true).ToListAsync();
        }

        public async Task AddStudentAsync(Student student)
        {
            await Students.InsertOneAsync(student);
        }

        public async Task<bool> IsStudentIdExistsAsync(string studentId)
        {
            var count = await Students.CountDocumentsAsync(s => s.StudentId == studentId);
            return count > 0;
        }

        public IMongoCollection<Attendance> Attendances => _database.GetCollection<Attendance>("attendances");

        public async Task<List<Attendance>> GetAllAttendancesAsync()
        {
            return await Attendances.Find(_ => true).ToListAsync();
        }

        public async Task AddAttendanceAsync(Attendance attendance)
        {
            await Attendances.InsertOneAsync(attendance);
        }

        public async Task<List<Attendance>> GetAttendanceByDateAsync(DateTime date)
        {
            return await Attendances.Find(a => a.Date.Date == date.Date).ToListAsync();
        }
    }
}
