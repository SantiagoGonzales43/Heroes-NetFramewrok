using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticaAdoNet.Services;
using System.Threading.Tasks;
using PracticaAdoNet.DTOs;

namespace PracticaAdoNet.Controllers
{
    public class HeroeController : Controller
    {
        private readonly IHeroeService _heroeService;

        public HeroeController()
        {
            _heroeService = new HeroeService();
        }
        // GET: Heroe
        public async Task<ActionResult> Index()
        {
            var heroes = await _heroeService.GetAllHeroes();
            return View(heroes);
        }

        //Get: Create Heroe
        [HttpGet]
        public ActionResult CreateHeroe()
        {
            return View(new CreateHeroeDtos());
        }

        //POST: Create Heroe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateHeroe(CreateHeroeDtos heroe)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var newIdHeroe = await _heroeService.CreateHeroe(heroe);
                    TempData["MensajeExito"] = $"Heroe con nombre = {heroe.Nombre} y id = {newIdHeroe} creado con exito";
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear el héroe: " + ex.Message);
                    return View(heroe); 
                }
                
            }else
            {
                return View(heroe);
            }
        }
    }
}