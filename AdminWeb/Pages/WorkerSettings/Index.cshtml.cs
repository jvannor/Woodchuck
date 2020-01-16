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
    public class IndexModel : PageModel
    {
        private readonly UtilityLib.Data.WoodchuckDbContext _context;

        public IndexModel(UtilityLib.Data.WoodchuckDbContext context)
        {
            _context = context;
        }

        public IList<Models.WorkerSettings> WorkerSettings { get;set; }

        public async Task OnGetAsync()
        {
            WorkerSettings = await _context.WorkerSettings.ToListAsync();
        }
    }
}
