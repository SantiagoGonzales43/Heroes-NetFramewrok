using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using PracticaAdoNet.Models.ViewModels;
using PracticaAdoNet.DTOs;
namespace PracticaAdoNet.Services
{
    public interface IHeroeService 
    {
        Task<List<HeroeViewModels>> GetAllHeroes();
        Task<int> CreateHeroe(CreateHeroeDtos heroe);
        Task<HeroeViewModels> GetHeroeById(int id);
    }
}