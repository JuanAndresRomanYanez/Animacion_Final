using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class Accion
    {
        public List<object> objeto { get; set; }
        public int tiempo { get; set; }
        public Accion(List<object> accion, int tiempoSegundos)
        {
            objeto = accion;
            tiempo = tiempoSegundos;
        }
        public Accion()
        {
            this.objeto = new List<object>();
            this.tiempo = 0;
        }
    }
}
