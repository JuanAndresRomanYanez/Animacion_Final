using OpenTK;
using Proyecto1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class Parte
    {
        public List<Poligono> Lista { get; set; }
        Punto p { get; set; }
        public Punto origin { get; set; }
        public Parte()
        {
            Lista = new List<Poligono>();
            origin = GetCentro();
        }
        public Parte(float x, float y, float z)
        {
            this.p = new Punto(x, y, z);
            Lista = new List<Poligono>();
            origin = GetCentro();
        }
        public Parte(Parte p1)
        {
            Lista = new List<Poligono>();
            for (int i = 0; i < p1.Lista.Count; i++)
            {
                Lista.Add(new Poligono(p1.Lista[i], p1.Lista[i].color));
            }
            origin = GetCentro();
        }

        public void Adicionar(Poligono poligono)
        {

            Lista.Add(poligono);
        }

        public void Dibujar()
        {
            for (int i = 0; i < Lista.Count; i++)
            {
                Lista.ElementAt(i).Dibujar();
            }
        }

        public void Eliminar(int i)
        {
            Lista.RemoveAt(i);
        }
        public void mover(Punto p)
        {
            foreach (Poligono pol in Lista)
            {
                pol.mover(p);
            }
        }
        public void Escalar(float scale)
        {
            Escalar(scale, origin);
        }
        public void Escalar(float scale, Punto origen)
        {
            foreach (Poligono poligono in Lista)
            {
                poligono.Escalar(scale, origen);
            }
        }
        public void Trasladar(float x, float y, float z)
        {
            foreach (Poligono poligono in Lista)
            {
                poligono.Trasladar(x, y, z);
            }
        }
        public void Rotar(float angle, char c)
        {
            Rotar(angle, c, origin);
        }
        public void Rotar(float angle, char c, Punto origen)
        {
            foreach (Poligono poligono in Lista)
            {
                poligono.Rotar(angle, c, origen);
            }
        }
        public void SetCentro(float x, float y, float z)
        {
            origin.acumular(x, y, z);
        }
        public Punto GetCentro()
        {
            int numPoligonos = Lista.Count;
            Punto origen = new Punto(0, 0, 0);
            foreach (Poligono pol in Lista)
            {
                Punto centroPoligono = new Punto(pol.GetCentro());
                origen.x += centroPoligono.x;
                origen.y += centroPoligono.y;
                origen.z += centroPoligono.z;
            }
            origen.x /= numPoligonos;
            origen.y /= numPoligonos;
            origen.z /= numPoligonos;

            return origen;
        }
    }
}
