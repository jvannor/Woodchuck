using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UtilityLib.Data;
using Models=UtilityLib.Models;

namespace AdminWeb.Pages.CameraSettings
{
    public class DeleteModel : PageModel
    {
        private readonly UtilityLib.Data.WoodchuckDbContext _context;

        public DeleteModel(UtilityLib.Data.WoodchuckDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.CameraSettings CameraSettings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CameraSettings = await _context.CameraSettings.FirstOrDefaultAsync(m => m.Id == id);

            if (CameraSettings == null)
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

            CameraSettings = await _context.CameraSettings.FindAsync(id);

            if (CameraSettings != null)
            {
                _context.CameraSettings.Remove(CameraSettings);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
