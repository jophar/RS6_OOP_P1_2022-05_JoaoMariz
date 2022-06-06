using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    /*
     * Classe User para criação do objecto com as informações sobre o utilizador.
     * Não tem metodos para operar as propriedades dado que o programa não tem backoffice
     * 
     */

    internal class User
    {
        internal string Id { get; set; }
        internal string UserName { get; set; }
        internal string Nome { get; set; }
        internal string PassWord { get; set; }

        /*
         * Construtores
         */

        internal User()
        {
            Id = "0";
            UserName = string.Empty;
            Nome = string.Empty;
            PassWord = string.Empty;
        }

        internal User (string id, string userName, string nome, string password)
        {
            Id = id;
            UserName = userName;
            Nome = nome;
            PassWord = password;
        }
    }
}
