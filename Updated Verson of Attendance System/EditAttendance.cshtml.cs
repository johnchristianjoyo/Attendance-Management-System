using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AttendanceSystem2.Models;
using AttendanceSystem2.Services;

namespace AttendanceSystem2.Pages
{
    public class EditAttendanceModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        public EditAttendanceModel(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [BindProperty]
        public Attendance Attendance { get; set; } = new Attendance();

        // Load attendance data when page opens
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("/Record");
            }

            var attendance = await _mongoDbService.GetAttendanceByIdAsync(id);

            if (attendance == null)
            {
                TempData["Error"] = "Attendance record not found.";
                return RedirectToPage("/Record");
            }

            Attendance = attendance;
            return Page();
        }

        // Save updated attendance data
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Attendance.Status))
            {
                TempData["Error"] = "Please select a status.";
                return Page();
            }

            try
            {
                // Update MarkedAt to current time when edited
                Attendance.MarkedAt = DateTime.Now;

                // Update attendance in database
                await _mongoDbService.UpdateAttendanceAsync(Attendance);

                TempData["Message"] = $"Attendance for {Attendance.FullName} updated successfully!";

                // Redirect back to Record page
                return RedirectToPage("/Record");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating attendance: {ex.Message}";
                return Page();
            }
        }
    }
}
