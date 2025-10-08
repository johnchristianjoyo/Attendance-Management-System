using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AttendanceSystem2.Models;
using AttendanceSystem2.Services;

namespace AttendanceSystem2.Pages
{
    public class DeleteStudentModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        public DeleteStudentModel(MongoDbService mongoDbService)
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

        // Delete student when form is submitted
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Delete student from database
                await _mongoDbService.DeleteStudentAsync(Student.Id);

                TempData["Message"] = $"Student {Student.FullName} deleted successfully!";

                // Redirect back to Attendances page
                return RedirectToPage("/Attendances");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error deleting student: {ex.Message}";
                return Page();
            }
        }
    }
}
