using Proyecto1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class Escenario
    {
        public Dictionary<string, Objeto> objetos { get; set; }
        public Punto origin { get; set; }
        public Escenario()
        {
            objetos = new Dictionary<string, Objeto>();
            origin = GetCentro();
        }
        public Escenario(Escenario esc){
            objetos = new Dictionary<string, Objeto>();
            foreach (KeyValuePair<string, Objeto> k in esc.objetos){
                objetos.Add((k.Key), new Objeto(k.Value));
            }
            origin = GetCentro();
        }
        public void AdicionarObjeto(String s, Objeto p)
        {

            objetos.Add(s, p);
        }
        public void Dibujar()
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.Dibujar();
            }
        }
        public void mover(Punto p)
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.mover(p);
            }
        }
        public void Rotar(float angle, char dir)
        {
            Rotar(angle, dir, origin);
        }
        public void Rotar(float angle, char dir, Punto origen)
        {

            foreach (Objeto valor in objetos.Values)
            {
                valor.Rotar(angle, dir, origen);
            }

        }
        public void Escalar(float scale)
        {
            Escalar(scale, origin);
        }
        public void Escalar(float scale, Punto origen)
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.Escalar(scale, origen);
            }
        }
        public void Trasladar(float x, float y, float z)
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.Trasladar(x, y, z);
            }
        }
        public Punto GetCentro()
        {
            int numObjetos = objetos.Count;
            Punto origen = new Punto(0, 0, 0);
            foreach (Objeto valor in objetos.Values)
            {
                Punto centroObjeto = new Punto(valor.GetCentro());
                origen.x += centroObjeto.x;
                origen.y += centroObjeto.y;
                origen.z += centroObjeto.z;
            }
            origen.x /= numObjetos;
            origen.y /= numObjetos;
            origen.z /= numObjetos;

            return origen;
        }

    }
}
