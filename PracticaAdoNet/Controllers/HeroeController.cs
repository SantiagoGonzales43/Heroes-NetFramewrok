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


        //Get por id
        public async Task<ActionResult> GetHeroeById(int id)
        {
            if (id == null || id <= 0) 
            {
                TempData["ErrorMessage"] = "El ID del héroe es inválido o no se ha proporcionado.";
                return RedirectToAction("Index"); 
            }

            try
            {
                
                var heroeEncontrado = await _heroeService.GetHeroeById(id);  

                if (heroeEncontrado == null)
                {
                    TempData["ErrorMessage"] = $"No se encontró un héroe con ID: {id}.";
                    return RedirectToAction("Index"); 
                }
                else
                {
               
                    return View(heroeEncontrado);
                }
            }
            catch (Exception ex)
            {
                
                TempData["ErrorMessage"] = $"Ocurrió un error al buscar el héroe con ID {id}: {ex.Message}";
                // Loguea la excepción para depuración
                Console.WriteLine($"Error al obtener héroe por ID: {ex}");
                return RedirectToAction("Index"); // Redirige a la página principal con el mensaje de error
            }
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

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<ActionResult> DeleteHeroeById(int id) 
        {
            if (id <= 0) 
            {
                TempData["ErrorMessage"] = "ID de héroe inválido para eliminación.";
                return RedirectToAction("Index");
            }

            try
            {
                var rowsAffected = await _heroeService.DeleteHeroeById(id);

                if (rowsAffected > 0) 
                {
                    TempData["MensajeExito"] = $"Héroe con ID {id} eliminado exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = $"No se encontró un héroe con ID {id} o no se pudo eliminar.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ocurrió un error al eliminar el héroe con ID {id}: {ex.Message}";
                Console.WriteLine($"Error al eliminar héroe: {ex}");
            }

           
            return RedirectToAction("Index");
        }

        //Update heroe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateHeroe(UpdateHeroeDtos heroe)
        {

            if(ModelState.IsValid)
            {
                var filasAfectadas = await _heroeService.UpdateHeroe(heroe);
                if(filasAfectadas <= 0)
                {
                    TempData["ErrorMessage"] = $"No se ha podido actualizar su heroe por que su heroe con id = {heroe.Id} no existe";
                    return RedirectToAction("Index");
                }
                TempData["MensajeExito"] = $"Se logro actualizar su heroe con id = {heroe.Id}";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Porfavor rellene bien el formulario";
                return View(heroe);
            }
        } 

        //get para formulario
        public async Task<ActionResult> FormularioEdicion(int id)
        {
            var datosHeroe = await _heroeService.GetHeroeById(id);

            return View(datosHeroe);
        }
    }
}