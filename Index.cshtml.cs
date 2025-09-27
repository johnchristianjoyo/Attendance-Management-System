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
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (await _mongoDbService.IsStudentIdExistsAsync(Student.StudentId))
                {
                    TempData["Error"] = $"Student ID '{Student.StudentId}' already exists!";
                    return Page();
                }

                await _mongoDbService.AddStudentAsync(Student);

                TempData["Message"] = $"Student {Student.FullName} added successfully!";

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
