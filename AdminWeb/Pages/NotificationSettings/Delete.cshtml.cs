using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UtilityLib.Data;
using Models=UtilityLib.Models;

namespace AdminWeb.Pages.NotificationSettings
{
    public class DeleteModel : PageModel
    {
        private readonly UtilityLib.Data.WoodchuckDbContext _context;

        public DeleteModel(UtilityLib.Data.WoodchuckDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.NotificationSettings NotificationSettings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NotificationSettings = await _context.NotificationSettings.FirstOrDefaultAsync(m => m.Id == id);

            if (NotificationSettings == null)
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

            NotificationSettings = await _context.NotificationSettings.FindAsync(id);

            if (NotificationSettings != null)
            {
                _context.NotificationSettings.Remove(NotificationSettings);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
