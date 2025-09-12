using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Models;
using Pokedex.ViewModels;

namespace Pokedex.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;

    public HomeController(ILogger<HomeController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        HomeVM home = new()
        {
            Pokemons = _db.Pokemons
                .Include(p => p.Tipos)
                .ThenInclude(t => t.Tipo)
                .ToList(),
            Tipos = _db.Tipos.ToList()
        };
        return View(home);
    }

    public IActionResult Details(int id)
    {
        Pokemon pokemon = _db.Pokemons
            .Where(p => p.Numero == id) //usar ele melhora a performance e rapidez da pesquisa (por causa dos filtros)
            .Include(p => p.Regiao)
            .Include(p => p.Genero)
            .Include(p => p.Tipos)
            .ThenInclude(t => t.Tipo)
            .SingleOrDefault();
        
        DetailsVM detail = new()
        {
            Atual = pokemon,
            Anterior = _db.Pokemons
                .OrderByDescending(p => p.Numero)
                .FirstOrDefault(p => p.Numero < id),
            Proximo = _db.Pokemons
                .OrderBy(p => p.Numero)
                .FirstOrDefault(p => p.Numero > id)
        };
        return View(detail);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
