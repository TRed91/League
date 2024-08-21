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
    public class PlayerModel : PageModel
    {
        private readonly LeagueContext _context;

        public Player Player { get; private set; }

        public PlayerModel( LeagueContext context )
        {
            _context = context;
        }

        public async Task OnGetAsync(string id)
        {
            Player = await _context.Players.FindAsync(id);
        }
    }
}