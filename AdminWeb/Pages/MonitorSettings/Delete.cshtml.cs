using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UtilityLib.Data;
using Models=UtilityLib.Models;

namespace AdminWeb.Pages.MonitorSettings
{
    public class DeleteModel : PageModel
    {
        private readonly UtilityLib.Data.WoodchuckDbContext _context;

        public DeleteModel(UtilityLib.Data.WoodchuckDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.MonitorSettings MonitorSettings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MonitorSettings = await _context.MonitorSettings.FirstOrDefaultAsync(m => m.Id == id);

            if (MonitorSettings == null)
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

            MonitorSettings = await _context.MonitorSettings.FindAsync(id);

            if (MonitorSettings != null)
            {
                _context.MonitorSettings.Remove(MonitorSettings);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
