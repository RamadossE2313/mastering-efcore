using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor_pages.Data;
using razor_pages.Models;

namespace razor_pages.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentContext _context;

        public IndexModel(StudentContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
        }
    }
}
