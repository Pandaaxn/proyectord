using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectord
{
    internal class Program
    {
        public enum Menu
        {
            consultar = 1, Depositar, Retirar
        }
        static double saldo = 0;
        static Dictionary<DateTime, double> depositos = new Dictionary<DateTime, double>();
        static Dictionary<DateTime, double> retiros = new Dictionary<DateTime, double>();
        static void Main(string[] args)
        {
            int intentos = 3;
            do
            {
                if (loggin())
                {
                    Console.WriteLine("Bienvenido");
                    while (true)
                    {
                        switch (men())
                        {
                            case Menu.consultar:
                                Console.WriteLine($"Tu saldo es{saldo}");
                                break;
                            case Menu.Depositar:
                                Console.WriteLine("cantidad a depositar");
                                double dep = Convert.ToDouble(Console.ReadLine());
                                saldo += dep;
                                break;


                        }
                    }
                }
                else
                {
                    intentos--;
                    Console.WriteLine($"Fallaste te quedan {intentos} ");
                }
                if (intentos == 0)
                {
                    Console.WriteLine("No te quedan intentos");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
            }
            while (intentos >= 0);
        }

        static bool loggin()
        {
            DateTime fechaact = DateTime.Now;

            Console.WriteLine("User");
            string user = Console.ReadLine();
            Console.WriteLine("Password");
            string password = Console.ReadLine();
            Console.WriteLine("Fecha de nacimiento");
            DateTime fechadenacimiento = Convert.ToDateTime(Console.ReadLine());
            int edad = fechaact.Year - fechadenacimiento.Year;

            if (user == "Diane" && password == "1234" && edad >= 18)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static Menu men()
        {
            Console.WriteLine("1) Consultar saldo actual");
            Console.WriteLine("2) Depositar dinero");
            Console.WriteLine("3) Retirar dinero");
            Console.WriteLine("4) Consultar historial de depósito");
            Console.WriteLine("5) Consultar historial de retiros");
            Console.WriteLine("6) Salir");
            Console.Write("Selecciona una opción: ");
            Menu opc = (Menu)Convert.ToInt32(Console.ReadLine());
            return opc;
        }
    }
}
