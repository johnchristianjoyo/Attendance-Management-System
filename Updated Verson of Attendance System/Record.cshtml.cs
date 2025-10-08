using Microsoft.AspNetCore.Mvc.RazorPages;
using AttendanceSystem2.Models;
using AttendanceSystem2.Services;

namespace AttendanceSystem2.Pages
{
    public class RecordModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        // Constructor - dependency injection to get database service
        public RecordModel(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // Property to hold all attendance records
        public List<Attendance> AttendanceRecords { get; set; } = new List<Attendance>();

        // Property to count present records
        public int PresentCount { get; set; }

        // Property to count absent records
        public int AbsentCount { get; set; }

        // OnGetAsync runs when page loads
        public async Task OnGetAsync()
        {
            try
            {
                // Get all attendance records from database
                AttendanceRecords = await _mongoDbService.GetAllAttendancesAsync();

                // Sort by date (newest first), then by marked time
                AttendanceRecords = AttendanceRecords
                    .OrderByDescending(a => a.Date)
                    .ThenByDescending(a => a.MarkedAt)
                    .ToList();

                // Calculate statistics
                PresentCount = AttendanceRecords.Count(a => a.Status == "Present");
                AbsentCount = AttendanceRecords.Count(a => a.Status == "Absent");
            }
            catch (Exception ex)
            {
                // Handle any errors
                AttendanceRecords = new List<Attendance>();
                PresentCount = 0;
                AbsentCount = 0;
            }
        }
    }
}
