using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajando_con_XML
{
    public class Contacto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Numero { get; set; }
        public enum Telefonia { Tigo, Claro}
        public Telefonia TipodeTelefonia { get; set; }

        public Contacto(string nombre, string apellido, string numero, Telefonia tipodeTelefonia)
        {
            Nombre = nombre;
            Apellido = apellido;
            Numero = numero;
            TipodeTelefonia = tipodeTelefonia;
        }

    }
}
