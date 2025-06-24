using System;
using System.Collections.Generic;
using System.Linq;
using PracticaAdoNet.Domain;
using PracticaAdoNet.DAL;
using PracticaAdoNet.Models.ViewModels;
using System.Threading.Tasks;
using PracticaAdoNet.DTOs;

namespace PracticaAdoNet.Services
{
    public class HeroeService : IHeroeService
    {
       private readonly IHeroeRepository _heroeRepository;

        public HeroeService()
        {
            _heroeRepository = new HeroeRepository();
        }

        public async Task<List<HeroeViewModels>> GetAllHeroes()
        {
            var domainHeroes = await _heroeRepository.GetAllHeroes();
            var heroesTransformado = new List<HeroeViewModels>();
            foreach (var heroe in domainHeroes)
            {
                heroesTransformado.Add(new HeroeViewModels
                {
                    Id = heroe.Id,
                    Nombre = heroe.Nombre,
                    Clase = heroe.Clase,
                    Nivel = heroe.Nivel
                });
            }


            return heroesTransformado;
        }

        public async Task<int> CreateHeroe(CreateHeroeDtos heroe) {

            return await _heroeRepository.CreateHeroe(heroe);
        }

        public async Task<HeroeViewModels> GetHeroeById(int id)
        {
            var heroeDomain = await _heroeRepository.GetHeroeById(id);

            if(heroeDomain == null)
            {
                return null;
            }


            var heroeTransformado = new HeroeViewModels {
                Nombre = heroeDomain.Nombre,
                Clase = heroeDomain.Clase,
                Nivel = heroeDomain.Nivel             
            };

            return heroeTransformado;

        }

        public async Task<int> DeleteHeroeById(int id)
        {
            var idHeroeEliminado = await _heroeRepository.DeleteHeroeById(id);

            return idHeroeEliminado;
        }

        public async Task<int> UpdateHeroe(UpdateHeroeDtos heroe)
        {
            var heroeDomain = new Heroe {
                Id = heroe.Id,
                Nombre = heroe.Nombre,
                Clase = heroe.Clase,
                Nivel = heroe.Nivel
            };

            var filasAfectadas = await _heroeRepository.UpdateHeroe(heroeDomain);

            return filasAfectadas;
        }
    }
}