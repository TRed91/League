using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using League.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using League.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace League.Pages.Teams
{
    public class TeamModel : PageModel
    {
        private readonly LeagueContext _context;

        public Team Team { get; set; }

        public List<Player> Players { get; set; }

        public TeamModel( LeagueContext context )
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Players = await _context.Players.ToListAsync();
				Team = await _context.Teams.FindAsync(id);
                return Page();
			}
            else
            {
                return NotFound();
            }
        }
    }
}