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
            {"M", "123asd32"}
        };

        /* 
         * Metodo Login que recebe as strings user e pass
         * Caso seja válida a informação introduzida, altera a variável
         * currentUser da classe RSGym tornando válidas as introduções
         * e manipulação de pedidos
         */
        internal static void Login(string user, string pass)
        {
            foreach(User i in RSGym.utilizadores)
            {
                if(i.UserName == user && i.PassWord == pass)
                {
                    string authUser = $"{user} autenticado com sucesso\n";
                    Console.WriteLine($"\n{new String('*', authUser.Length-1)}");
                    Console.WriteLine(authUser);
                    Console.WriteLine($"{new String('*', authUser.Length-1)}\n");
                    RSGym.currentUser = user;
                    return;
                }
            }

            Console.WriteLine("Username ou Password errados, por favor tente novamente\n");
        }

        internal static void Logout()
        {
            if (RSGym.currentUser != "RSGymPT")
                RSGym.currentUser = "RSGymPT";
            else
            {
                Console.Write("Nenhum utilizador com login efetuado\n");
            }
        }
    }
}
