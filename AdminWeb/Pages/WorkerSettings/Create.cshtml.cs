using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UtilityLib.Data;
using Models=UtilityLib.Models;

namespace AdminWeb.Pages.WorkerSettings
{
    public class CreateModel : PageModel
    {
        private readonly UtilityLib.Data.WoodchuckDbContext _context;

        public CreateModel(UtilityLib.Data.WoodchuckDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.WorkerSettings WorkerSettings { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WorkerSettings.Add(WorkerSettings);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
