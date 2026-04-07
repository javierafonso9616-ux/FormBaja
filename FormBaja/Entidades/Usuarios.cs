using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBaja.Entidades
{
    public class Usuarios
    {
        // persona
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        // lista de programas
        // public Dictionary<string, string> Programas { get; set; } = new Dictionary<string, string>();



        public Usuarios() { }

    }
}
