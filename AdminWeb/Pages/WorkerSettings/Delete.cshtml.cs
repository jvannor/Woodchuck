using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UtilityLib.Data;
using Models=UtilityLib.Models;

namespace AdminWeb.Pages.WorkerSettings
{
    public class DeleteModel : PageModel
    {
        private readonly UtilityLib.Data.WoodchuckDbContext _context;

        public DeleteModel(UtilityLib.Data.WoodchuckDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.WorkerSettings WorkerSettings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkerSettings = await _context.WorkerSettings.FirstOrDefaultAsync(m => m.Id == id);

            if (WorkerSettings == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkerSettings = await _context.WorkerSettings.FindAsync(id);

            if (WorkerSettings != null)
            {
                _context.WorkerSettings.Remove(WorkerSettings);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
