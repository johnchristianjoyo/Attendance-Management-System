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

        // Student operations
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

        // Check if full name already exists (for duplicate prevention)
        public async Task<bool> IsFullNameExistsAsync(string fullName)
        {
            var count = await Students.CountDocumentsAsync(s => s.FullName == fullName);
            return count > 0;
        }

        // Get single student by ID
        public async Task<Student?> GetStudentByIdAsync(string id)
        {
            return await Students.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        // Update student
        public async Task UpdateStudentAsync(Student student)
        {
            await Students.ReplaceOneAsync(s => s.Id == student.Id, student);
        }

        // Delete student
        public async Task DeleteStudentAsync(string id)
        {
            await Students.DeleteOneAsync(s => s.Id == id);
        }

        // Attendance operations
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

        // Get single attendance record by ID
        public async Task<Attendance?> GetAttendanceByIdAsync(string id)
        {
            return await Attendances.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        // Update attendance record
        public async Task UpdateAttendanceAsync(Attendance attendance)
        {
            await Attendances.ReplaceOneAsync(a => a.Id == attendance.Id, attendance);
        }
    }
}
