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
            Console.WriteLine("Comando não reconhecido. Use \"help\" para obter ajuda\n");
        }

        internal static bool RandomizarAulaAceite()
        {
            Random r = new Random();
            int n = r.Next(0,2);

            if (n == 1)
                return true;
            else
                return false;
        }
    }
}
