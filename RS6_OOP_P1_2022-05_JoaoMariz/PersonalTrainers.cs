using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    /*
     * Classe PersonalTrainers que contem a informação
     * sobre os nomes dos PT's e Horarios disponíveis
     */

    internal class PersonalTrainers
    {
        internal string Nome { get; set; }
        internal List<DateTime> Horarios { get; set; }

        internal PersonalTrainers()
        {
            string Nome = string.Empty;
            List<DateTime> Horarios = new List<DateTime>();
        }

        internal PersonalTrainers(string n, List<DateTime> d)
        {
            string Nome = n;
            List<DateTime> Horarios = d;
        }
    }
}
