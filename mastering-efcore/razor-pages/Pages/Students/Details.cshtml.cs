using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor_pages.Data;
using razor_pages.Models;

namespace razor_pages.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly Data.DataAnnotations.StudentContext _context;

        public DetailsModel(Data.DataAnnotations.StudentContext context)
        {
            _context = context;
        }

        public Models.DataAnnodations.Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            else
            {
                Student = student;
            }
            return Page();
        }
    }
}
