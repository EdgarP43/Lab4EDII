using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Lab3EDII.CifradosT
{
    public class ZigZag
    {
        public List<char> cifrado(List<char> Caracteres, int Clave)
        {
            List<char> MensajeCifrado = new List<char>();
            int recorrido = Clave - 2;
            recorrido = Clave + recorrido;
            int CuentaDeVueltas = Clave - 2;
            int CaractereAagregar = 1;
            int recorridoDos = 2;

            if (Clave == 1)
            {
                return Caracteres;
            }
            while (CaractereAagregar < Caracteres.Count)
            {
                CaractereAagregar += recorrido;
            }

            if (CaractereAagregar != Caracteres.Count)
            {
                int CaracterAgregado = CaractereAagregar - Caracteres.Count;
                for (int i = 0; i < CaracterAgregado; i++)
                {
                    Caracteres.Add('*');
                }
            }
            for (int i = 0; i < Caracteres.Count; i += recorrido)
            {
                if (i > Caracteres.Count)
                {
                    break;
                }
                MensajeCifrado.Add(Caracteres[i]);
            }
            int indice = 1;
            recorrido -= 2;
            int j = 0;
            while (CuentaDeVueltas > 0)
            {
                MensajeCifrado.Add(Caracteres[indice]);
                j = indice;
                while (j < Caracteres.Count)
                {
                    if (j + recorrido >= Caracteres.Count)
                    {
                        break;
                    }
                    j += recorrido;
                    MensajeCifrado.Add(Caracteres[j]);
                    if (j + recorridoDos >= Caracteres.Count)
                    {
                        break;
                    }
                    j += recorridoDos;
                    MensajeCifrado.Add(Caracteres[j]);

                }
                CuentaDeVueltas--;
                recorrido -= 2;
                recorridoDos += 2;
                indice++;


            }
            recorrido = Clave - 2;
            recorrido = Clave + recorrido;
            for (int h = Clave - 1; h < Caracteres.Count; h += recorrido)
            {
                if (h > Caracteres.Count)
                {
                    break;
                }
                MensajeCifrado.Add(Caracteres[h]);
            }
            return MensajeCifrado;

        }

        public List<char> Descifrar(List<char> TextoCifrado, int Clave)
        {
            List<char> TextoDescrifrado = new List<char>();
            Queue<char> CaracteresDeBajada = new Queue<char>();
            Queue<char> CaracteresDeSubida = new Queue<char>();
            int recorrido = (Clave - 2) + Clave;
            int CaractereAagregar = 1;


            if (Clave == 1) { return TextoCifrado; }
            while (CaractereAagregar < TextoCifrado.Count)
            {
                CaractereAagregar += recorrido;
            }

            if (CaractereAagregar != TextoCifrado.Count)
            {
                int CaracterAgregado = CaractereAagregar - TextoCifrado.Count;
                for (int i = 0; i < CaracterAgregado; i++)
                {
                    TextoCifrado.Add('*');
                }
            }

            int m = (TextoCifrado.Count + (-1 * (-1 - ((Clave - 2) * (2))))) / (((Clave - 2) * 2) + 2);
            int intermedios = (m - 1) * 2;
            int subidas = m;
            int ContarIteraciones = 0;
            int CIntermedios = 0;
            int auxiliar;
            int intervalos = 2;


            while (ContarIteraciones < m - 1)
            {
                auxiliar = subidas;
                CaracteresDeBajada.Enqueue(TextoCifrado[ContarIteraciones]);
                CaracteresDeBajada.Enqueue(TextoCifrado[subidas]);
                while (CIntermedios < Clave - 3)
                {
                    auxiliar += intermedios;
                    CaracteresDeBajada.Enqueue(TextoCifrado[auxiliar]);

                    CIntermedios++;
                }
                intervalos = CIntermedios;
                ContarIteraciones++;
                subidas += 2;
                CIntermedios = 0;


            }
            CaracteresDeBajada.Enqueue(TextoCifrado[m - 1]);

            ContarIteraciones = 0;
            int Inicio = TextoCifrado.Count - (m - 1);

            int SegundaCadena = (Inicio - intermedios) + 1;
            int IntermediosDeSubida = 0;
            CIntermedios = 0;
            while (ContarIteraciones < m - 1)
            {
                CaracteresDeSubida.Enqueue(TextoCifrado[Inicio]);
                CaracteresDeSubida.Enqueue(TextoCifrado[SegundaCadena]);
                IntermediosDeSubida = SegundaCadena;
                while (CIntermedios < Clave - 3)
                {
                    IntermediosDeSubida -= intermedios;
                    CaracteresDeSubida.Enqueue(TextoCifrado[IntermediosDeSubida]);

                    CIntermedios++;
                }
                ContarIteraciones++;
                Inicio++;
                SegundaCadena += 2;
                CIntermedios = 0;
            }
            ContarIteraciones = 0;
            intervalos += 2;

            while (ContarIteraciones < m - 1)
            {
                for (int i = 0; i < intervalos; i++)
                {
                    TextoDescrifrado.Add(CaracteresDeBajada.Dequeue());
                }
                for (int i = 0; i < intervalos; i++)
                {
                    TextoDescrifrado.Add(CaracteresDeSubida.Dequeue());
                }
                ContarIteraciones++;

            }

            List<char> listaRetorno = new List<char>();
            foreach (var item in TextoDescrifrado)
            {
                if (item != '*')
                {
                    listaRetorno.Add(item);
                }
            }

            return listaRetorno;
        }
    }
}
