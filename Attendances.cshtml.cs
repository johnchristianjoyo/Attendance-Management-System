using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AttendanceSystem2.Models;
using AttendanceSystem2.Services;

namespace AttendanceSystem2.Pages
{
    public class AttendanceRecord
    {
        public string StudentId { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Section { get; set; } = "";
        public DateTime Date { get; set; }
        public string Status { get; set; } = "";
    }

    public class AttendancesModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        public AttendancesModel(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [BindProperty]
        public List<Student> Students { get; set; } = new List<Student>();

        [BindProperty]
        public List<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

        public async Task OnGetAsync()
        {
            try
            {
                Students = await _mongoDbService.GetAllStudentsAsync();

                AttendanceRecords = Students.Select(s => new AttendanceRecord
                {
                    StudentId = s.StudentId,
                    FullName = s.FullName,
                    Section = s.Section,
                    Date = DateTime.Now.Date,
                    Status = ""
                }).ToList();
            }
            catch (Exception ex)
            {
                TempData["AttendanceMessage"] = $"Error loading students: {ex.Message}";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Students = await _mongoDbService.GetAllStudentsAsync();

                var attendanceCount = 0;
                var presentCount = 0;
                var absentCount = 0;

                for (int i = 0; i < AttendanceRecords.Count && i < Students.Count; i++)
                {
                    if (!string.IsNullOrEmpty(AttendanceRecords[i].Status))
                    {
                        var attendance = new Attendance
                        {
                            StudentId = Students[i].StudentId,
                            FullName = Students[i].FullName,
                            Section = Students[i].Section,
                            Date = DateTime.Now.Date,
                            Status = AttendanceRecords[i].Status
                        };

                        await _mongoDbService.AddAttendanceAsync(attendance);

                        attendanceCount++;
                        if (AttendanceRecords[i].Status == "Present")
                            presentCount++;
                        else if (AttendanceRecords[i].Status == "Absent")
                            absentCount++;
                    }
                }

                TempData["AttendanceMessage"] = $"Attendance saved! {attendanceCount} records processed. Present: {presentCount}, Absent: {absentCount}";

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                TempData["AttendanceMessage"] = $"Error saving attendance: {ex.Message}";
                return RedirectToPage();
            }
        }
    }
}
