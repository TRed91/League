using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using League.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using League.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace League.Pages.Teams
{
	public class IndexModel : PageModel
	{
		private readonly LeagueContext _context;

		public List<Team> Teams { get; set; }

		public List<Division> Divisions { get; set; }
        public List<Conference> Conferences { get; set; }

		[BindProperty(SupportsGet = true)]
		public string SelectFavorite { get; set; }

        public IndexModel (LeagueContext context)
		{
			_context = context;
		}

		public async Task OnGetAsync()
		{
			var teams = from team in _context.Teams
						select team;

			var divisionQuery = from division in _context.Divisions
								select division;

            var conferenceQuery = from conference in _context.Conferences
                                select conference;

            Divisions = await divisionQuery.ToListAsync();

            Conferences = await conferenceQuery.ToListAsync();

			if (!string.IsNullOrEmpty(SelectFavorite))
			{
				HttpContext.Session.SetString("favorite", SelectFavorite);
			}

			teams = teams.OrderBy(t => (t.Win / t.Loss));
			
			Teams = await teams.ToListAsync();
			Teams.Reverse();
		}
	}
}
