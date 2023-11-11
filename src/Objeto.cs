using OpenTK.Input;
using Proyecto1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class Objeto
    {
        public Dictionary<string, Parte> partes { get; set; }
        public Punto p { get; set; }
        public Punto origin { get; set; }
        public Objeto()
        {
            partes = new Dictionary<string, Parte>();
            origin = GetCentro();
        }
        public Objeto(float x, float y, float z)
        {
            this.p = new Punto(x, y, z);
            partes = new Dictionary<string, Parte>();
            origin = GetCentro();
        }
        public Objeto(Objeto obj)
        {
            partes = new Dictionary<string, Parte>();
            foreach (KeyValuePair<string, Parte> k in obj.partes)
            {
                partes.Add((k.Key), new Parte(k.Value));
            }
            origin = GetCentro();
        }
        public void AdicionarParte(String s, Parte p)
        {
            ActualizarParte(p);
            partes.Add(s, p);
        }
        public void ActualizarParte(Parte p)
        {
            foreach(Poligono pol in p.Lista){
                foreach(Punto pun in pol.puntos){
                    pun.acumular(this.p.x, this.p.y, this.p.z);
                    //pun.acumular(this.p);
                }
            }
        }
        public void EliminarParte(String s)
        {
            partes.Remove(s);
        }

        public void Dibujar()
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Dibujar();
            }
        }
        
        public void mover(Punto p)
        {
            foreach (Parte par in partes.Values)
            {
                par.mover(p);
            }
        }
        public void Escalar(float scale)
        {
            Escalar(scale, origin);
        }
        public void Escalar(float scale, Punto origen)
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Escalar(scale, origen);
            }
        }
        public void Rotar(float angle, char c)
        {
            Rotar(angle, c, origin);
        }
        public void Rotar(float angle, char c, Punto origen)
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Rotar(angle, c, origen);
            }
        }
        public void Trasladar(float x, float y, float z)
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Trasladar(x, y, z);
            }
        }
        public void SetCentro(float x, float y, float z)
        {
            origin.acumular(x, y, z);
        }
        public Punto GetCentro()
        {
            int numPartes = partes.Count;
            Punto origen = new Punto(0, 0, 0);
            foreach (Parte par in partes.Values)
            {
                Punto centroParte = new Punto(par.GetCentro());
                origen.x += centroParte.x;
                origen.y += centroParte.y;
                origen.z += centroParte.z;
            }
            origen.x /= numPartes;
            origen.y /= numPartes;
            origen.z /= numPartes;

            return origen;
        }
    }
}
