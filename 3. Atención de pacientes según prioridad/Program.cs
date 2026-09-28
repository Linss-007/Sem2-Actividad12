using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.Atención_de_pacientes_según_prioridad
{
    public enum nivelesAtencion
    {
        Emergencia,
        Urgente,
        ConsultaNormal
    }
    class pacientes
    {
        private string nombre;
        private int edad;
        private string motivoConsulta;
        private nivelesAtencion nivelAtencion;
        public pacientes(string nombreIng, int edadIng, string motivoConsultaIng, nivelesAtencion nivelAtencionIng)
        {
            Nombre = nombreIng;
            Edad = edadIng;
            MotivoConsulta = motivoConsultaIng;
            NivelAtencion = nivelAtencionIng;
        }
        public nivelesAtencion NivelAtencion
        {
            get { return nivelAtencion; }
            set { nivelAtencion = value; }
        }

        public string MotivoConsulta
        {
            get { return motivoConsulta; }
            set { motivoConsulta = value; }
        }

        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

    }
    internal class Program
    {
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Atencion de pacientes.");
            Console.WriteLine("1. Registrar paciente.");
            Console.WriteLine("2. Atender a siguiente.");
            Console.WriteLine("3. Consultar que paciente se va a atender.");
            Console.WriteLine("4. Mostrar pacientes pendientes.");
            Console.WriteLine("5. Salir.");
            Console.WriteLine("Elija una opción.");
        }
        static void Main(string[] args)
        {
            int opcion;
            Stack<pacientes> pacientes = new Stack<pacientes>();
            do
            {
                Menu();
            MalOp:
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Error: La opción ingresada no es válida.");
                    goto MalOp;
                }
                switch(opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Ingrese el nombre del paciente");
                        string nombre = Console.ReadLine();
                    MalEdad:
                        Console.WriteLine("Ingrese la edad del paciente");
                        if(!int.TryParse(Console.ReadLine(), out int edad))
                        {
                            Console.WriteLine("Error: La edad no es válida");
                            goto MalEdad;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Acción registrada.");
                        Console.ResetColor();
                        Console.WriteLine("Presione cualquier tecla para continuar.");
                        Console.ReadKey();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            } while (opcion != 5);
        }
    }
}
