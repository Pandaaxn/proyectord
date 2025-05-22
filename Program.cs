using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace proyectord
{
    internal class Program
    {
        public enum Menu
        {
            consultar = 1, Depositar, Retirar, HistorialDepositos, HistorialRetiros, Salir
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
                    Console.WriteLine("---- Bienvenido al Banco PNDAX ----");
                    while (true)
                    {
                        switch (men())
                        {
                            case Menu.consultar:
                                Console.WriteLine($"Tu saldo actual es de : {saldo} ");
                                break;
                            case Menu.Depositar:
                                Depositar();
                                break;

                            case Menu.Retirar:
                                retirar();
                                break;

                            case Menu.HistorialDepositos:
                                historialdep();
                                break;

                            case Menu.HistorialRetiros:
                               historialrep();  
                                break;

                            case Menu.Salir:
                                salir();
                                return;

                            default:
                                Console.WriteLine(" Opción inválida -Elije una opción del 1-6-");
                                break;
                        }
                    }
                }
                else
                {
                    intentos--;
                    Console.WriteLine($"Fallaste te quedan {intentos} intentos ");
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
            Console.WriteLine("Fecha de nacimiento dd/MM/yyyy");
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

        static void enviarComprobante(string tipo, DateTime fecha, double monto)
        {
            Console.Write("¿Deseas recibir un comprobante por correo? (s/n): ");
            string respuesta = Console.ReadLine().ToLower();

            if (respuesta == "s")
            {
                EnviarComprobantePorCorreo(tipo, fecha, monto);
            }
        }
        static void EnviarComprobantePorCorreo(string tipo, DateTime fecha, double monto)
        {
            string remitente = "113449@alumnouninter.mx";
            string contraseña = "Lkjhmn27";
            string destinatario = "dianegh22@gmail.com";

            StringBuilder cuerpo = new StringBuilder();
            cuerpo.AppendLine("----- Comprobante de transacción ------");
            cuerpo.AppendLine($"Tipo de operación: {tipo}");
            cuerpo.AppendLine($"Monto: ${monto}");
            cuerpo.AppendLine($"Fecha y hora: {fecha}");

            MailMessage mensaje = new MailMessage(remitente, destinatario, $"Comprobante de {tipo}", cuerpo.ToString());

            SmtpClient cliente = new SmtpClient("smtp.office365.com", 587)
            {
                Credentials = new NetworkCredential(remitente, contraseña),
                EnableSsl = true
            };

            try
            {
                cliente.Send(mensaje);
                Console.WriteLine("Comprobante enviado exitosamente al correo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error al enviar el comprobante: " + ex.Message);
            }
        }

        static void retirar()
        {
            Console.Write("Ingresa la cantidad a retirar: ");
            double retiro = Convert.ToDouble(Console.ReadLine());

            if (retiro <= saldo)
            {
                saldo -= retiro;
                DateTime fechaRetiro = DateTime.Now;
                retiros.Add(fechaRetiro, retiro);
                Console.WriteLine($" Retiro de ${retiro} realizado con éxito.");

                enviarComprobante("Retiro", fechaRetiro, retiro);
            }
            else
            {
                Console.WriteLine(" Fondos insuficientes.");
            }

        }

        static void Depositar()
        {
            Console.WriteLine("Cantidad a depositar");
            double dep = Convert.ToDouble(Console.ReadLine());
            saldo += dep;
            DateTime fechaDeposito = DateTime.Now;
            depositos.Add(fechaDeposito, dep);
            Console.WriteLine($" Depósito de $ {dep} realizado con éxito.");

            enviarComprobante("Depósito", fechaDeposito, dep);
        }

        static void historialdep()
        {
            Console.WriteLine(" Historial de Depósitos:");
            foreach (var item in depositos)
            {
                Console.WriteLine($" - {item.Key}: ${item.Value}");
            }
        }
        static void historialrep()
        {
            Console.WriteLine(" Historial de Retiros:");
            foreach (var item in retiros)
            {
                Console.WriteLine($" - {item.Key}: ${item.Value}");
            }
        }

        static void salir()
        {
            Environment.Exit(0);
        }

    }
}
