using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using League.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using League.Models;
using Microsoft.EntityFrameworkCore;

namespace League.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly LeagueContext _context;

    public League.Models.League League { get; set; }

    public IndexModel(ILogger<IndexModel> logger, LeagueContext context)
    {
      _logger = logger;
      _context = context;
    }

    public async Task OnGetAsync()
    {
            League = await _context.Leagues.FirstOrDefaultAsync();
    }
  }
}
