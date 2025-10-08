using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AttendanceSystem2.Models;
using AttendanceSystem2.Services;

namespace AttendanceSystem2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MongoDbService _mongoDbService;

        public IndexModel(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [BindProperty]
        public Student Student { get; set; } = new Student();

        public void OnGet()
        {
            // This runs when page loads
        }

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
                // Check if student ID already exists
                if (await _mongoDbService.IsStudentIdExistsAsync(Student.StudentId))
                {
                    TempData["Error"] = $"Student ID '{Student.StudentId}' already exists!";
                    return Page();
                }

                // Check if full name already exists
                if (await _mongoDbService.IsFullNameExistsAsync(Student.FullName))
                {
                    TempData["Error"] = $"Student name '{Student.FullName}' already exists!";
                    return Page();
                }

                // Add student to database
                await _mongoDbService.AddStudentAsync(Student);

                TempData["Message"] = $"Student {Student.FullName} added successfully!";

                // Clear the form
                Student = new Student();

                return Page();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error adding student: {ex.Message}";
                return Page();
            }
        }
    }
}
