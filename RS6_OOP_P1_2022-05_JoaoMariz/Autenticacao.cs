using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    /*
     * Classe Autenticação que valida o login e 
     * manipula a variavel na classe RSGym para o CLI
     * saber quem está loggado
     */
    internal class Autenticacao
    {
        /*
         * Variavel com as informações de Login dos utilizadores
         * Numa segunda versão com backoffice, poderei alterar a proteção do field
         * para permitir a inserção de novos utilizadores
         */

        private readonly static Dictionary<string, string> users = new Dictionary<string, string>
        {
            {"JMF", "1a2b3c4d"},
            {"MRZ", "123asd32"}
        };

        /* 
         * Metodo Login que recebe as strings user e pass
         * Caso seja válida a informação introduzida, altera a variável
         * currentUser da classe RSGym tornando válidas as introduções
         * e manipulação de pedidos
         */
        internal static void Login(string user, string pass)
        {
            foreach(KeyValuePair<string,string> i in users)
            {
                if(i.Key == user && i.Value == pass)
                {
                    Console.WriteLine($"{user} autenticado com sucesso\n");
                    RSGym.currentUser = user;
                    return;
                }
            }

            Console.WriteLine("Username ou Password errados, por favor tente novamente\n");
        }

        internal static void LoginCheck()
        {
            if (RSGym.currentUser.Equals("RSGym"))
            {
                Console.WriteLine("Por favor efetue login na consola\n");
                return;
            }
        }
    }
}
