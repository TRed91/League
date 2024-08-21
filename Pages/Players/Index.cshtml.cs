using System.Collections.Generic;
using System.Threading.Tasks;
using League.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using League.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace League.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly LeagueContext _context;

        public List<Player> Players{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedTeam { get; set; }

        [BindProperty(SupportsGet =true)]
        public string SelectedPosition { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; } = "Name";

        public SelectList Teams {  get; set; }
		public SelectList Position { get; set; }

		public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get all players
            var players = from p in _context.Players
                          select p;

            // Get all teams ordered by teamId and assign them to Teams
            var teamQuery = from t in _context.Teams
                            orderby t.TeamId
                            select t.TeamId;

            Teams = new SelectList(await teamQuery.ToListAsync());
            Position = new SelectList(players.Select(player => player.Position));

            if (!string.IsNullOrEmpty(SelectedPosition))
                players = players.Where(player => player.Position == SelectedPosition);

			// If SelectedTeam isn't empty only select player where TeamId == SelectedTeam
			if (!string.IsNullOrEmpty(SelectedTeam))
                players = players.Where(player => player.TeamId == SelectedTeam);

            if (!string.IsNullOrEmpty(SearchString))
                players = players.Where(player => player.Name.Contains(SearchString));

            switch (SortField)
            {
                case "Name": players = players.OrderBy(player => player.Name); break;
                case "TeamId": players = players.OrderBy(player => player.TeamId); break;
                case "Position": players = players.OrderBy(player => player.Position); break;
            }

            Players = await players.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("./Index");
        }
    }
}

