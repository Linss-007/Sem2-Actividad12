using _4.Procesamiento_de_documentos_con_reintentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _4.Procesamiento_de_documentos_con_reintentos
{
    class Documento
    {
        private string nombreDocumento;

        public string NombreDocumento
        {
            get { return nombreDocumento; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Error: El nombre de documento no puede ir vacío.");
                }
                else
                {
                    nombreDocumento = value;
                }
            }
        }
        private int cantidadPaginas;

        public int CantidadPaginas
        {
            get { return cantidadPaginas; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine($"Error: Un documento no puede tener {value} páginas, por favor ingrese una cantidad mayor a 0. ");
                }
                else
                {
                    cantidadPaginas = value;
                }
            }
        }
        private string nombreUsuario;

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Error: el nombre de usuario del documento no puede ir vacío.");
                }
                else
                {
                    nombreUsuario = value;
                }
            }
        }
        private int intentos;
        public int Intentos
        {
            get { return intentos; }
            set { intentos = value; }
        }

        public bool PuedeReintentar
        {
            get { return intentos < 3; }
        }

        public Documento(string nombreDocumento, int cantidadPaginas, string nombreUsuario)
        {
            this.NombreDocumento = nombreDocumento;
            this.CantidadPaginas = cantidadPaginas;
            this.NombreUsuario = nombreUsuario;
            this.intentos = 0;
        }

        public void MostrarInfo()
        {
            Console.WriteLine("+========================================================================+");
            Console.WriteLine($"+ Nombre del documento: {NombreDocumento}");
            Console.WriteLine($"+ Cantidad de páginas: {CantidadPaginas} páginas.");
            Console.WriteLine($"+ Nombre del Usuario: {NombreUsuario}");
            Console.WriteLine($"+ Intentos realizados: {Intentos}/3");
            Console.WriteLine("+========================================================================+");
            Console.WriteLine();
        }
    }


}
internal class Program
{
    static void Main(string[] args)
    {

        Queue<Documento> pendientes = new Queue<Documento>();
        List<Documento> impresos = new List<Documento>();
        List<Documento> rechazados = new List<Documento>();
        int opcion = 0;
        bool valido = true;
        string nombreDocumento = "";
        string nombreUsuario = "";
        int cantidadPaginas = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("+==========================================+");
            Console.WriteLine("+   SISTEMA DE IMPRESIÓN CON REINTENTOS    +");
            Console.WriteLine("+==========================================+");
            Console.WriteLine("+ 1. Agregar documento a la cola.          +");
            Console.WriteLine("+ 2. Procesar siguiente documento.         +");
            Console.WriteLine("+ 3. Ver documentos impresos.              +");
            Console.WriteLine("+ 4. Ver documentos rechazados.            +");
            Console.WriteLine("+ 5. Ver documentos pendientes.            +");
            Console.WriteLine("+ 6. Salir.                                +");
            Console.WriteLine("+==========================================+");
            Console.WriteLine();
            Console.Write("Por favor, ingrese una opción: ");
            valido = int.TryParse(Console.ReadLine(), out opcion);
            Console.WriteLine();
            if (!valido)
            {
                Console.WriteLine();
                Console.WriteLine("Error: Por favor ingrese un número.");
            }
            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("=== AGREGAR DOCUMENTO ===");
                    Console.WriteLine();
                    do
                    {
                        Console.Write("Ingrese el nombre del documento: ");
                        nombreDocumento = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nombreDocumento))
                        {
                            Console.WriteLine("Error: El nombre no puede estar vacío.\n");
                        }
                    } while (string.IsNullOrWhiteSpace(nombreDocumento));
                    bool paginasValidas;
                    do
                    {
                        Console.Write("Ingrese la cantidad de páginas: ");
                        paginasValidas = int.TryParse(Console.ReadLine(), out cantidadPaginas);
                        if (!paginasValidas)
                        {
                            Console.WriteLine("Error: Tipo de dato incorrecto, ingrese un número.\n");
                        }
                        else if (cantidadPaginas <= 0)
                        {
                            Console.WriteLine("Error: La cantidad debe ser mayor a 0.\n");
                            paginasValidas = false;
                        }
                    } while (!paginasValidas);
                    do
                    {
                        Console.Write("Ingrese el nombre del usuario: ");
                        nombreUsuario = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nombreUsuario))
                        {
                            Console.WriteLine("Error: El nombre de usuario no puede estar vacío.\n");
                        }
                    } while (string.IsNullOrWhiteSpace(nombreUsuario));
                    Documento nuevo = new Documento(nombreDocumento, cantidadPaginas, nombreUsuario);
                    pendientes.Enqueue(nuevo);
                    Console.WriteLine();
                    Console.WriteLine(" Documento agregado a la cola correctamente.");
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("=== PROCESAR SIGUIENTE DOCUMENTO ===");
                    Console.WriteLine();
                    if (pendientes.Count == 0)
                    {
                        Console.WriteLine("No hay documentos pendientes en la cola.");
                        Console.ReadKey();
                        break;
                    }
                    Documento actual = pendientes.Dequeue();
                    Console.WriteLine("Documento a imprimir:");
                    Console.WriteLine($"{actual.NombreDocumento} ({actual.CantidadPaginas} páginas) - Usuario: {actual.NombreUsuario}");
                    Console.WriteLine($"Intento #{actual.Intentos + 1} de 3");
                    Console.WriteLine();
                    Console.Write("¿La impresión fue exitosa? (s/n): ");
                    string respuesta = Console.ReadLine();
                    Console.WriteLine();
                    if (respuesta != null && respuesta.Trim().ToLower() == "s")
                    {
                        impresos.Add(actual);
                        Console.WriteLine($"✅ Documento '{actual.NombreDocumento}' impreso correctamente.");
                    }
                    else
                    {
                        actual.Intentos++;
                        if (actual.PuedeReintentar)
                        {
                            pendientes.Enqueue(actual);
                            Console.WriteLine($"Falló la impresión. Intento {actual.Intentos}/3.");
                            Console.WriteLine($"El documento vuelve al final de la cola.");
                        }
                        else
                        {
                            rechazados.Add(actual);
                            Console.WriteLine($"Documento rechazado tras 3 intentos fallidos.");
                        }
                    }

                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("=== DOCUMENTOS IMPRESOS ===");
                    Console.WriteLine();
                    if (impresos.Count == 0)
                    {
                        Console.WriteLine("No hay documentos impresos aún.");
                    }
                    else
                    {
                        foreach (Documento d in impresos)
                        {
                            d.MostrarInfo();
                        }
                        Console.WriteLine($"Total de documentos impresos: {impresos.Count}");
                    }
                    Console.ReadKey();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("=== DOCUMENTOS RECHAZADOS ===");
                    Console.WriteLine();
                    if (rechazados.Count == 0)
                    {
                        Console.WriteLine("No hay documentos rechazados aún.");
                    }
                    else
                    {
                        foreach (Documento d in rechazados)
                        {
                            d.MostrarInfo();
                        }
                        Console.WriteLine($"Total de documentos rechazados: {rechazados.Count}");
                    }
                    Console.ReadKey();
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine("=== DOCUMENTOS PENDIENTES EN COLA ===");
                    Console.WriteLine();
                    if (pendientes.Count == 0)
                    {
                        Console.WriteLine("No hay documentos pendientes en la cola.");
                    }
                    else
                    {
                        int pos = 1;
                        foreach (Documento d in pendientes)
                        {
                            Console.WriteLine($"[{pos}]");
                            d.MostrarInfo();
                            pos++;
                        }
                        Console.WriteLine($"Total de documentos pendientes: {pendientes.Count}");
                    }
                    Console.ReadKey();
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Cerrando el programa...");
                    Console.WriteLine();
                    Console.WriteLine("=== RESUMEN FINAL ===");
                    Console.WriteLine($"Documentos impresos:   {impresos.Count}");
                    Console.WriteLine($"Documentos rechazados: {rechazados.Count}");
                    Console.WriteLine($"Documentos pendientes: {pendientes.Count}");
                    Console.WriteLine();
                    Console.WriteLine("¡Hasta luego!");
                    break;
            }

        } while (opcion != 6);
    }
}

