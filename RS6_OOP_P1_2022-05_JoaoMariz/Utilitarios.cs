using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    internal class Utilitarios
    {
        internal static void AjudaInfo()
        {
            Console.WriteLine("Comando não reconhecido. Use \"help\" para obter ajuda");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
