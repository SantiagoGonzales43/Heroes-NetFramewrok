using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;

namespace PracticaAdoNet.DTOs
{
    public class CreateHeroeDtos 
    {
        [Required(ErrorMessage ="El nombre del heroe debe de ser obligatorio")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="El nombre de su heroe debe de tener entre 2 y 50 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La clase del heroe debe de ser obligatoria")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "La clase de su heroe debe de tener entre 2 y 50 caracteres")]
        public string Clase { get; set; }
        [Required(ErrorMessage = "El nivel del heroe debe de ser obligatorio")]
        [Range(1,1000,ErrorMessage ="El nivel de su heroe debe estar entre 1 y 1000 niveles")]
        public int Nivel { get; set; }

    }
}