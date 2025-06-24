using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PracticaAdoNet.Domain;
using PracticaAdoNet.DTOs;

namespace PracticaAdoNet.DAL
{
    public interface IHeroeRepository 
    {
        Task<List<Heroe>> GetAllHeroes();
        Task<int> CreateHeroe(CreateHeroeDtos heroe);
        Task<Heroe> GetHeroeById(int id);
        Task<int> DeleteHeroeById(int id);
        Task<int> UpdateHeroe(Heroe heroe);
    }
}