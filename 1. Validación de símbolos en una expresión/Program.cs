using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.Validación_de_símbolos_en_una_expresión
{
    internal class Program
    {
        static void ValidarExpresion(string expresion)
        {
            Stack<(char simbolo, int posicion)> pila = new Stack<(char, int)>();
            for (int i = 0; i < expresion.Length; i++)
            {
                char c = expresion[i];
                if (c == '(' || c == '[' || c == '{')
                {
                    pila.Push((c, i));
                }
                else if (c == ')' || c == ']' || c == '}')
                {
                    if (pila.Count == 0)
                    {
                        Console.WriteLine($"Incorrecta: símbolo de cierre '{c}' sin apertura en la posición {i}.");
                        return;
                    }
                    char apertura = pila.Peek().simbolo;
                    if (!EsPareja(apertura, c))
                    {
                        int posApertura = pila.Peek().posicion;
                        Console.WriteLine($"Incorrecta: se esperaba el cierre de '{apertura}' (posición {posApertura}) " + $"pero se encontró '{c}' en la posición {i}.");
                        return;
                    }
                    pila.Pop();
                }
            }
            if (pila.Count > 0)
            {
                var (simbolo, pos) = pila.Peek();
                Console.WriteLine($"Incorrecta: el símbolo '{simbolo}' abierto en la posición {pos} nunca fue cerrado.");
                return;
            }
            Console.WriteLine("Correcta: todos los símbolos están bien abiertos, cerrados y anidados.");
        }
        static bool EsPareja(char apertura, char cierre)
        {
            return (apertura == '(' && cierre == ')') ||
                   (apertura == '[' && cierre == ']') ||
                   (apertura == '{' && cierre == '}');
        }
        static void Main(string[] args)
        {
            bool valido;
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== VALIDADOR DE SÍMBOLOS DE AGRUPACIÓN ===");
                Console.WriteLine("Ingrese expresiones matemáticas para validar.");
                Console.WriteLine();
                Console.Write("Ingrese una expresión: ");
                string expresion = Console.ReadLine();
                Console.WriteLine();
                ValidarExpresion(expresion);
                Console.WriteLine();
                Console.ReadKey();
                do
                {
                    Console.Clear();
                    Console.WriteLine("=======================================");
                    Console.WriteLine("1. Agregar más expresiones.");
                    Console.WriteLine("2. Salir.");
                    Console.WriteLine("=======================================");
                    Console.WriteLine();
                    Console.Write("Por favor, ingrese una opción: ");
                    valido = int.TryParse(Console.ReadLine(), out opcion);
                    Console.WriteLine();
                    if (!valido || (opcion != 1 && opcion != 2))
                    {
                        Console.WriteLine("Error: Opción inválida. Ingrese 1 o 2.");
                        Console.WriteLine();
                        Console.ReadKey();
                        valido = false;
                    }
                } while (!valido);
            } while (opcion != 2);
        }
    }
}
