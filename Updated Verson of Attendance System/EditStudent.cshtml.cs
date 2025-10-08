using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AttendanceSystem2.Models;
using AttendanceSystem2.Services;

namespace AttendanceSystem2.Pages
{
    public class EditStudentModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        public EditStudentModel(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [BindProperty]
        public Student Student { get; set; } = new Student();

        // Load student data when page opens
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("/Attendances");
            }

            var student = await _mongoDbService.GetStudentByIdAsync(id);

            if (student == null)
            {
                TempData["Error"] = "Student not found.";
                return RedirectToPage("/Attendances");
            }

            Student = student;
            return Page();
        }

        // Save updated student data
        public async Task<IActionResult> OnPostAsync()
        {
            // Check if all fields are filled
            if (string.IsNullOrWhiteSpace(Student.FullName) ||
                string.IsNullOrWhiteSpace(Student.Section) ||
                string.IsNullOrWhiteSpace(Student.StudentId))
            {
                TempData["Error"] = "All fields are required.";
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Update student in database
                await _mongoDbService.UpdateStudentAsync(Student);

                TempData["Message"] = $"Student {Student.FullName} updated successfully!";

                // Redirect back to Attendances page
                return RedirectToPage("/Attendances");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error updating student: {ex.Message}";
                return Page();
            }
        }
    }
}
