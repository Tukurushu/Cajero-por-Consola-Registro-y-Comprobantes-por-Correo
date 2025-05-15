using System;
using System.Collections.Generic;
using System.Linq;
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
            Salir //
        }
        static Dictionary<int, string> DepositosI = new Dictionary<int, string>();
        static void Main(string[] args)
        {
            while (true)
            {
                switch (Men())
                {
                    case Menu.Consultar_Saldo_actual:

                        break;


                }

            }
        }
        static Menu Men()
        {
            Console.WriteLine("Bienvenido al Cajero por Consola");
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1) Consultar Saldo actual");
            Console.WriteLine("2) Depositar dinero");
            Console.WriteLine("3) Retirar dinero");
            Console.WriteLine("4) Revisar Historial de depositos");
            Console.WriteLine("5) Revisar historial de retiros");
            Console.WriteLine("6) Salir");
            Menu opc = (Menu)Convert.ToInt32(Console.ReadLine());
            return opc;
        }
        static void Consultar()
        {
         
            
        }
    }
}
