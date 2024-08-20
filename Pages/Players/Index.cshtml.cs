using System.Collections.Generic;
using System.Threading.Tasks;
using League.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using League.Models;

namespace League.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public List<Player> Players{ get; set; }

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Players = await _context.Players.ToListAsync();
            return Page();
        }
    }
}

