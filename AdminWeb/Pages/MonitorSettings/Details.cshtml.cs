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
    public class DetailsModel : PageModel
    {
        private readonly UtilityLib.Data.WoodchuckDbContext _context;

        public DetailsModel(UtilityLib.Data.WoodchuckDbContext context)
        {
            _context = context;
        }

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
    }
}
