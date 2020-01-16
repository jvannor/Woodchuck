using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UtilityLib.Data;
using Models=UtilityLib.Models;

namespace AdminWeb.Pages.WorkerSettings
{
    public class EditModel : PageModel
    {
        private readonly UtilityLib.Data.WoodchuckDbContext _context;

        public EditModel(UtilityLib.Data.WoodchuckDbContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WorkerSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerSettingsExists(WorkerSettings.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WorkerSettingsExists(int id)
        {
            return _context.WorkerSettings.Any(e => e.Id == id);
        }
    }
}
