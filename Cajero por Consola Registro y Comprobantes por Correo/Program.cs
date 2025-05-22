using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Cajero_por_Consola_Registro_y_Comprobantes_por_Correo
{
    internal class Program
    //Oscar Sedano Rivera//
    {
        public enum Menu
        {

            Consultar_Saldo_actual = 1, //
            Depositar_dinero, //
            Retirar_dinero, //
            Revisar_Historial_depositos, //
            Revisar_historial_retiros, //
            EnviarCorreo,                    //  
            Salir //
        }
        static double saldo = 0;
        static Dictionary<DateTime, double> Depositos = new Dictionary<DateTime, double>();
        static Dictionary<DateTime, double> Retiros = new Dictionary<DateTime, double>();
        static void Main(string[] args)
        {
            int intentos = 3;
            do
            {
                if (IniciarSesion())
                {
                    Console.WriteLine("Bienvenido al cajero por consola");
                    while (true)
                    {
                        switch (Men())
                        {
                            case Menu.Consultar_Saldo_actual:
                                Console.WriteLine($"Tu saldo es {consultar()}");
                                break;
                            case Menu.Depositar_dinero:
                                Depositar();
                                break;
                            case Menu.Retirar_dinero:
                                retirar();
                                break;
                            case Menu.Revisar_Historial_depositos:
                                Historialdep();
                                break;
                            case Menu.Revisar_historial_retiros:
                                Historialret();
                                break;
                            case Menu.EnviarCorreo:
                                break;

                            case Menu.Salir:
                                Environment.Exit(1);
                                break;

                        }

                    }
                }


                else
                {
                    intentos--;
                    Console.WriteLine($"Fallaste te quedan : {intentos}");

                    if (intentos == 0)
                    {
                        Console.WriteLine("Has excedido el numero de intentos permitidos");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
            }
            while (intentos >= 0);
        }
        static double consultar()
        {
            return saldo;

        }
        static void Depositar()
        {
            Console.WriteLine("Cuanto vas a depositar");
            double dep = Convert.ToDouble(Console.ReadLine());
            Depositos.Add(DateTime.Now, dep);
            saldo += dep;
            Depositos.Add(DateTime.Now, dep);
            Console.WriteLine("deposito hecho con exito");
        }
        static void retirar()
        {

            Console.WriteLine("Cuanto vas a retirar");
            double ret = Convert.ToDouble(Console.ReadLine());
            if (ret > saldo)
            {
                Console.WriteLine("No tienes suficiente saldo");
            }
            else
            {
                saldo -= ret;
                Retiros.Add(DateTime.Now, ret);
                Console.WriteLine($"Has retirado: {ret}");
                Console.WriteLine("tu retiro ha sido realizado con exito");
            }
            
        }
        static void Historialdep()
        {
            Console.WriteLine("Depositos");
            foreach (var I in Depositos)
            {
                Console.WriteLine($"fecha deposito: {I.Key} Monto: {I.Value}");
            }
            Console.WriteLine("Deseas enviar por correo en historial de depositos?");
            Console.WriteLine("1)Si");
            Console.WriteLine("2)No");

            int opc = Convert.ToInt32(Console.ReadLine());
            if (opc == 1)
                enviarCorreo(Depositos);            
            else
            {
                Console.WriteLine("Regresando al menu..");
            }

        }
        static void Historialret()
        {
            Console.WriteLine("Retirar");
            foreach (var E in Retiros)
            {
                Console.WriteLine($"fecha retiro; {E.Key} Monto: {E.Value}");
            }
            Console.WriteLine("Deseas enviar correo en historial de retiros?");
            Console.WriteLine("1)Si");
            Console.WriteLine("2)No");

            int opc = Convert.ToInt32(Console.ReadLine());
            if (opc == 1)
                enviarCorreo(Retiros);
            else
            {
                Console.WriteLine("Regresando al menu..");
            }


        }
        static bool enviarCorreo(Dictionary<DateTime, double> transacciones)
        {
            string servidorSmtp = "smtp.office365.com";
            int puerto = 587;
            string usuario = "112912@alumnouninter.mx";
            string contraseña = "oskrjr8104@";

            SmtpClient smtp = new SmtpClient(servidorSmtp)
            {
                Port = puerto,
                Credentials = new NetworkCredential(usuario, contraseña),
                EnableSsl = true
            };
            MailMessage correo = new MailMessage()
            {
                From = new MailAddress(usuario),
                Subject = "Historial de transacciones",
                IsBodyHtml = false
            };

            string cuerpoMensaje = "Historial de transacciones:\n\n";
            foreach (var t in transacciones)
            {
                cuerpoMensaje += $"{t.Key}. {t.Value}\n";
            }
            correo.Body = cuerpoMensaje;
            correo.To.Add("112912@alumnouninter.mx");
            smtp.Send(correo);
            return true;
        }

        static Menu Men()
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1) Consultar Saldo actual");
            Console.WriteLine("2) Depositar dinero");
            Console.WriteLine("3) Retirar dinero");
            Console.WriteLine("4) Revisar Historial de depositos");
            Console.WriteLine("5) Revisar historial de retiros");
            Console.WriteLine("6) Enviar el correo");
            Console.WriteLine("7) Salir");
            Console.WriteLine("----------------------------------");
            Menu opc = (Menu)Convert.ToInt32(Console.ReadLine());
            return opc;
        }
        static bool IniciarSesion()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Dame usuario");
            Console.WriteLine("------------------");
            string usuario = Convert.ToString(Console.ReadLine());
            //
            Console.WriteLine("------------------");
            Console.WriteLine("Dame NIP");
            Console.WriteLine("------------------");
            string contraseña = Convert.ToString(Console.ReadLine());
            //
            Console.WriteLine("------------------");
            Console.WriteLine("Dame tu fecha de nacimiento dd/MM/yyyy");
            Console.WriteLine("------------------");
            DateTime fecha = Convert.ToDateTime(Console.ReadLine());
            DateTime fechaActual = DateTime.Now;
            int años = fechaActual.Year - fecha.Year;
            if (usuario == "Oscar" && contraseña == "oskrjr8104@" && años >= 18)
                return true;
            else
                Console.WriteLine("No cumples la edad");
            return false;
        }
    }
}
