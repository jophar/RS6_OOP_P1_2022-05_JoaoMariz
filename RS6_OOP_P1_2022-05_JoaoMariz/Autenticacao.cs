using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    internal class Autenticacao
    {
        // Variavel com as informações de Login dos utilizadores
        // Ver se é melhor fazer nova classe
        private static Dictionary<string, string> users = new Dictionary<string, string>
        {
            {"JMF", "1a2b3c4d"},
            {"MRZ", "123asd32"}
        };

        internal static void Login(string user, string pass)
        {
            foreach(KeyValuePair<string,string> i in users)
            {
                if(i.Key == user && i.Value == pass)
                {
                    Console.WriteLine($"{user} autenticado com sucesso");
                    Console.WriteLine("Pressionar qualquer tecla para continuar");
                    Console.ReadKey();
                    RSGym.currentUser = user;
                    break;
                }

                else
                {
                    Console.WriteLine("Username ou Password errados, por favor tente novamente");
                    Console.WriteLine("Pressionar qualquer tecla para continuar");
                    Console.ReadKey();
                }
            }
        }
    }
}
