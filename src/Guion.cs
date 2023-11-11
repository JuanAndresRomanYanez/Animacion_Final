using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Proyecto1_01
{
    public class Guion
    {
        public List<Accion> acciones { get; set; }
        private Thread ejecucionHilo;
        private bool pausado;
        private bool detener;

        public Guion()
        {
            acciones = new List<Accion>();
            ejecucionHilo = null;
            pausado = false;
            detener = false;
        }
        public Guion(Guion guion)
        {
            acciones = new List<Accion>();
            foreach(Accion act in guion.acciones)
            {
                acciones.Add(act);
            }
            ejecucionHilo = null;
            pausado = false;
            detener = false;
        }
        public void AgregarAccion(List<object> accion, int tiempo)
        {
            acciones.Add(new Accion(accion, tiempo));
        }

        public void Iniciar()
        {
            if (ejecucionHilo == null || !ejecucionHilo.IsAlive)
            {
                pausado = false;
                detener = false;
                ejecucionHilo = new Thread(EjecutarAcciones);
                ejecucionHilo.Start();
            }
            else if (pausado)
            {
                pausado = false;
            }
        }

        public void Pausar()
        {
            pausado = true;
        }

        public void Detener()
        {
            detener = true;
        }

        private void EjecutarAcciones()
        {
            foreach (Accion accion in acciones)
            {
                int tiempoRestante = accion.tiempo;

                while (tiempoRestante > 0)
                {
                    if (detener)
                    {
                        return;
                    }

                    if (!pausado)
                    {
                        // Ejecutar la acción
                        for (int i = 0; i < accion.objeto.Count; i++)
                        {
                            object obj = accion.objeto[i];
                            obj.GetType().GetMethod("Invoke").Invoke(obj, null);
                        }
                        // Esperar 1 segundo (1000 milisegundos)
                        Thread.Sleep(50);
                        tiempoRestante--;
                    }
                    else
                    {
                        Thread.Sleep(0);//Lo que se espera para la otra acción
                    }
                }
            }
        }
    }

}
