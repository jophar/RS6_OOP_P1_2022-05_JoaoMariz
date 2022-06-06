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
     * 
     * TODO: 
     *  - Seria interessante o programa verificar na lista de horarios dos PT's se o mesmo não colide com outra aula
     */

    internal class PersonalTrainer
    {
        internal string Id { get; set; }
        internal string Nome { get; set; }
        internal List<DateTime> Horarios { get; set; }

        internal PersonalTrainer()
        {
            Id = "0";
            Nome = string.Empty;
            Horarios = new List<DateTime>();
        }

        internal PersonalTrainer(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        internal PersonalTrainer(string id, string nome, List<DateTime> data)
        {
            Id = id;    
            Nome = nome;
            Horarios = data;
        }
    }
}
