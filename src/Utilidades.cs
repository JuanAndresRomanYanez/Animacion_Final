using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class Utilidades
    {
        public static void Guardar<T>(T objeto, string f)
        {
            string s = JsonConvert.SerializeObject(objeto);
            TextWriter Archivo = new StreamWriter($"{f}");
            Archivo.Write(s);
            Archivo.Close();
        }

        public static T Cargar<T>(string f)
        {
            T objeto = default(T);
            TextReader Leer = new StreamReader($"{f}");
            string s = Leer.ReadToEnd();
            objeto = JsonConvert.DeserializeObject<T>(s);
            Console.WriteLine(objeto);

            Leer.Close();
            return objeto;
        }
    }
}
