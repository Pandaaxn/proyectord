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
                        
                    }
                }
                else
                {
                    intentos--;
                    Console.WriteLine($"Fallaste te quedan {intentos} ");
                }
                if (intentos==0)
                {
                    Console.WriteLine("No te quedan intentos");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
            }
            while (intentos >=0);
        }

        static bool loggin()
        {
            DateTime fechaact = DateTime.Now;

            Console.WriteLine("User");
            string user=Console.ReadLine();
            Console.WriteLine("Password");
            string  password = Console.ReadLine();
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

    }
}
