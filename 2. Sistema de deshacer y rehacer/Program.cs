using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.Sistema_de_deshacer_y_rehacer
{
    internal class Program
    {
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Sistema de acciones.");
            Console.WriteLine("1. Registrar una acción.");
            Console.WriteLine("2. Deshacer la ultima acción.");
            Console.WriteLine("3. Rehacer la ultima acción deshecha.");
            Console.WriteLine("4. Mostrar acciones realizadas.");
            Console.WriteLine("5. Salir.");
            Console.WriteLine("Elija una opción. :D");
        }
        static void Main(string[] args)
        {
            int opcion;
            string accionAnterior = "";
            Stack<string>AccionesRealizadas = new Stack<string>();
            do
            {
                Menu();
            MalOp:
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Opción no válida.");
                    Console.ResetColor();
                    goto MalOp;
                }
                switch(opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Ingrese la acción a registrar");
                        string accion = Console.ReadLine();
                        AccionesRealizadas.Push(accion);
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("Acción registrada.");
                        Console.ResetColor();
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        if(AccionesRealizadas.Count > 0)
                        {
                            accionAnterior = AccionesRealizadas.Peek();
                            Console.WriteLine("Acción a deshacer");
                            Console.WriteLine(AccionesRealizadas.Pop());
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: No existen acciones registradas.");
                            Console.ResetColor();
                        }
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                    case 3:
                        if(AccionesRealizadas.Count > 0)
                        {
                            Console.WriteLine("Última acción restaurada:");
                            Console.WriteLine(accionAnterior);
                            AccionesRealizadas.Push(accionAnterior);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: No existen acciones registradas.");
                            Console.ResetColor();
                        }
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                    case 4:
                        if (AccionesRealizadas.Count > 0)
                        {
                            Console.WriteLine("Acciones registradas:");
                            foreach (string a in AccionesRealizadas)
                            {
                                Console.WriteLine(a);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Error: No existen acciones registradas.");
                            Console.ResetColor();
                        }
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("La opción no existe, intente de nuevo");
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 5);
        }
    }
}
