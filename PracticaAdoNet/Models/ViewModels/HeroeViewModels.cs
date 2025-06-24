using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PracticaAdoNet.Models.ViewModels
{
    public class HeroeViewModels 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clase { get; set; }
        public int Nivel { get; set; }
    }
}