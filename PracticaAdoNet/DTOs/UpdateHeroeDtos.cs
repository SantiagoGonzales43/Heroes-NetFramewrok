using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PracticaAdoNet.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PracticaAdoNet.DTOs
{
    public class UpdateHeroeDtos : CreateHeroeDtos
    {
        [Required(ErrorMessage = "El id del heroe es requerido")]
        public int Id { get; set; }
    }
}