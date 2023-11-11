using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Proyecto1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class Poligono
    {
        public List<Punto> puntos { get; set; }
        public Color4 color { get; set; }
        Punto p { get; set; }
        public Matrix4 m { get; set; }
        public Punto origin { get; set; }
        public Poligono()
        {
            puntos = new List<Punto>();
            this.color = color;
            m = Matrix4.Identity;
            origin = GetCentro();
        }
        public Poligono(Color4 color)
        {
            puntos = new List<Punto>();
            this.color = color;
            m = Matrix4.Identity;
            origin = GetCentro();
        }
        public Poligono(Color4 color, float x, float y, float z)
        {
            puntos = new List<Punto>();
            this.color = color;
            this.p = new Punto(x, y, z);
            m = Matrix4.Identity;
            origin = GetCentro();
        }
        public Poligono(Poligono p, Color4 color)
        {
            puntos = new List<Punto>();
            this.color = color;
            m = Matrix4.Identity;
            for (int i = 0; i < p.puntos.Count(); i++)
            {
                puntos.Add(new Punto(p.puntos[i]));
            }
            origin = GetCentro();
        }
        public void Dibujar()
        {
            PrimitiveType primitiveType = PrimitiveType.Polygon;
            GL.Begin(primitiveType);
            GL.Color4(color);

            for (int i = 0; puntos.Count > i; i++)
            {
                Vector4 res = puntos[i].ToVector4() * m;
                //puntos[i] = new Punto(res.X, res.Y, res.Z);
                GL.Vertex4(res);
            }
            //m = Matrix4.Identity;
            GL.End();
        }

        public void Adicionar(float x, float y, float z)
        {
            puntos.Add(new Punto(x + p.x, y + p.y, z + p.z));
        }

        public void Adicionar(Punto punto)
        {
            puntos.Add(punto);
        }
        public void Eliminar(int i)
        {
            puntos.RemoveAt(i);
        }
        public void mover(Punto p)
        {
            foreach (Punto pun in puntos)
            {
                pun.acumular(p);
            }
        }
        public void Escalar(float scale)
        {
            Escalar(scale, origin);
        }
        public void Escalar(float scale, Punto origen)
        {
            Matrix4 Tp = Matrix4.CreateTranslation(-origen.x, -origen.y, -origen.z);
            Matrix4 S = Matrix4.CreateScale(scale, scale, scale);
            Matrix4 T = Matrix4.CreateTranslation(origen.x, origen.y, origen.z);
            Matrix4 C = Tp * S * T;
            m = C * m;
            //Guardar los puntos despues de rotar
        }
        public void Rotar(float angle, char c)
        {
            Rotar(angle, c, origin);
        }
        public void Rotar(float angle, char c, Punto origen)
        {
            Matrix4 Tp = Matrix4.CreateTranslation(-origen.x, -origen.y, -origen.z);
            Matrix4 R = new Matrix4();
            switch (c)
            {
                case 'x':
                    R = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angle));
                    break;
                case 'y':
                    R = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle));
                    break;
                case 'z':
                    R = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(angle));
                    break;
                default: break;
            }
            Matrix4 T = Matrix4.CreateTranslation(origen.x, origen.y, origen.z);
            Matrix4 C = Tp * R * T;
            m = C * m;
        }

        public void Trasladar(float x, float y, float z)
        {
            m *= Matrix4.CreateTranslation(x, y, z);//la matriz 
        }
        public void SetCentro(float x, float y, float z)
        {
            origin.acumular(x, y, z);
        }

        public Punto GetCentro()
        {
            int numPuntos = puntos.Count;
            Punto origen = new Punto(0, 0, 0);
            foreach (Punto pun in puntos)
            {
                origen.x += pun.x;
                origen.y += pun.y;
                origen.z += pun.z;
            }
            origen.x /= numPuntos;
            origen.y /= numPuntos;
            origen.z /= numPuntos;

            return origen;
        }

    }

}
