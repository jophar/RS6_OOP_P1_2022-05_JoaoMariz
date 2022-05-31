using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    /* 
     * Classe Aula que contem a definição do objecto aula
     * e os métodos para manipulação dos mesmos
     */

    internal class Aula
    {
        internal Aula()
        {
            string PersonalTrainerName = string.Empty;
            string UserName = string.Empty;
            DateTime DataDaAula = DateTime.MinValue;
            bool AulaAceite = true;
            int NumeroDoPedido = 0;
        }

        internal Aula(string pt, string un, DateTime d, bool log, bool aa, int num)
        {
            string PersonalTrainerName = pt;
            string UserName = un;
            DateTime DataDaAula = d;
            bool AulaAceite = aa;
            int NumeroDoPedido = num;
        }

        internal static void Request() // ver se da para usar o outro metodo
        { }

        internal static void AnularAula()
        { }

        internal static void ConcluirAula()
        { }

        internal static void InserirMensagem()
        { }
    }
}
