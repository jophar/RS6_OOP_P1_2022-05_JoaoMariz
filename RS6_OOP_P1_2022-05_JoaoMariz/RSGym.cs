using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    /*
     * Classe RSGym que contem o objecto CLI e os metodos 
     * para as opções do CLI
     */

    class RSGym
    {
        /* 
         * Variavel onde vai ser "guardado" o utilizador que se encontra ativo
         * Por default está gravado o RSGym e que só irá permitir registos
         * caso seja diferente deste valor
         * internal dado que é manupulado pela classe Autenticação
         */

        internal static string currentUser = "JMF";


        /*
         * Escape flag da consola cujo loop exite no Program.cs
         * Caso seja evocado o metodo Sair(), altera a flag e o 
         * programa termina.
         * internal dado que é lido o valor no Program.cs
         */

        internal static bool exitFlag = true;


        /* 
         * Variavel helpMenu com a informação de todos os comandos.
         * Usada a flag "readonly" dado que não é para alterar o conteudo.
         */

        private static readonly Dictionary<string, string> helpMenu = new Dictionary<string, string>
        {
            {"help" ,       "Lista este menu de ajuda"},
            {"exit" ,       "Sair da aplicação"},
            {"clear",       "Limpa a consola"},
            {"login",       "Fazer login na aplicação.\n" +
                            "\t\t Utilização: login -u {username} -p {password}"},
            {"request",     "Fazer o pedido do PT indicando: nome do PT, dia e horas.\n" +
                            "\t\t Utilização: request -n {nome} -d {dd/mm/yyyy} -h {hh:mm}"},
            {"cancel",      "Anular um pedido\n" +
                            "\t\t Utilização: cancel -r {nº do pedido}"},
            {"finish",      "Dar informação que a aula foi concluida\n" +
                            "\t\t Utilização: finish -r {nº pedido}"},
            {"message",     "Mensagem a informar o motivo da não conclusão da aula\n" +
                            "\t\t Utilização: message -r {nº pedido} -s {assunto}"},
            {"myrequest",   "Lista o pedido efetuado\n" +
                            "\t\t Utilização: myrequest -r {nº pedido}"},
            {"requests",    "Listar todos os pedidos efetuados\n" +
                            "\t\t Utilização: requests -a" }
        };


        /*
         * Lista que vai receber todas as aulas introduzidas até ao momento
         */

        internal static Dictionary<int, Aula> aulas = new Dictionary<int, Aula>();


        private static Dictionary<string, List<DateTime>> personalTrainers = new Dictionary<string, List<DateTime>>()
        {
            { "Maria", new List<DateTime>() { DateTime.MinValue } },
            { "José", new List<DateTime>() { DateTime.MinValue } },
            { "MeninoJesus", new List<DateTime>() { DateTime.MinValue } }
        };


        // Metodo para lista o menu de ajuda com recurso ao Dictionary que é váriavel da Classe
        // usado o .Length para ficar tudo alinhado de acordo com a dimensão da primeira palavra
        // Se necessário adicionar mais item's, já fica formatado
        internal static void Ajuda()
        {
            foreach (KeyValuePair<string, string> item in helpMenu)
            {
                if (item.Key.Length < 8)
                    Console.WriteLine($"{item.Key}\t\t {item.Value}\n");
                else
                    Console.WriteLine($"{item.Key}\t {item.Value}\n");
            }
        }

        internal static void IniciarConsola()
        {
            string arg = string.Empty;
            Console.Write($"{currentUser} > ");
            arg = Console.ReadLine();
            ParseArgument(arg);
        }

        private static void ParseArgument(string arg)
        {

            string[] tmp = new string[0];
            tmp = arg.Split(' ');

            switch (tmp[0])
            {
                case "help": Ajuda(); break;
                case "exit": Sair(); break;
                case "clear": LimparConsola(); break;

                case "login":
                    {
                        string login, pass;
                        string patternLogin = @"^login -u (?<user>[a-zA-Z0-9]+) -p (?<pass>[a-zA-Z0-9]+)";

                        if (Regex.IsMatch(arg, patternLogin, RegexOptions.IgnoreCase))
                        {
                            Regex rx = new Regex(patternLogin, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            MatchCollection matches = rx.Matches(arg);

                            foreach (Match match in matches)
                            {
                                GroupCollection groups = match.Groups;
                                login = groups["user"].Value;
                                pass = groups["pass"].Value;

                                Autenticacao.Login(login, pass);
                            }
                        }

                        else
                            Utilitarios.AjudaInfo();
                    } break;

                case "request":
                    {
                        if (currentUser.Equals("RSGym"))
                        {
                            Console.WriteLine("Por favor efetue login na consola\n");
                            return;
                        }

                        string patternRequest = @"^request -n (?<nome>[A-zÀ-ú0-9]+) -d (?<data>[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]) -h (?<hora>[0-9][0-9]:[0-9][0-9]$)";

                        if (Regex.IsMatch(arg, patternRequest, RegexOptions.IgnoreCase))
                        {
                            Regex rx = new Regex(patternRequest, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            MatchCollection matches = rx.Matches(arg);

                            foreach (Match match in matches)
                            {
                                GroupCollection groups = match.Groups;

                                string[] d = groups["data"].Value.Split('/');
                                string[] h = groups["hora"].Value.Split(':');
                                int i = aulas.Count + 1;
                                string ptname = groups["nome"].Value;

                                DateTime t = new DateTime(Convert.ToInt32(d[2]), Convert.ToInt32(d[1]), Convert.ToInt32(d[0]), Convert.ToInt32(h[0]), Convert.ToInt32(h[1]), 0);

                                Console.WriteLine($"DATA GERADA: {t}");
                                Console.WriteLine($"VALOR A SER GRAVADO NO DIC {i}");
                                Console.WriteLine($"NOME DO PT {groups["nome"].Value}");

                                Aula a = new Aula(ptname, currentUser, t, Utilitarios.RandomizarAulaAceite(), i);
                                /*
                                a.PersonalTrainerName = ptname;
                                a.UserName = currentUser;
                                a.DataAula = t;
                                a.AulaAceite = Utilitarios.RandomizarAulaAceite();
                                a.NumeroDoPedido = i;
                                */
                                Console.WriteLine($"{a.PersonalTrainerName} // {a.NumeroDoPedido} // {a.DataAula}");

                                aulas.Add(i, a);

                                foreach (KeyValuePair<int, Aula> al in aulas)
                                {
                                    //if(a.UserName.Equals(currentUser) && a.AulaAceite)
                                    //{
                                    Console.WriteLine($"{al.Key} - {al.Value.PersonalTrainerName} - {al.Value.DataAula} - {al.Value.PersonalTrainerName}");
                                    //}
                                }
                                return;
                            }
                            Utilitarios.AjudaInfo();
                            return;
                        }
                        else
                        {
                            Utilitarios.AjudaInfo();
                        }
                    }; break;

                case "cancel":
                    {
                        if (currentUser.Equals("RSGym"))
                        {
                            Console.WriteLine("Por favor efetue login na consola\n");
                            return;
                        }

                        Aula.CancelarPedido(arg);
                    } break;

                case "requests":
                    {
                        if (currentUser.Equals("RSGym"))
                        {
                            Console.WriteLine("Por favor efetue login na consola\n");
                            return;
                        }

                        /*foreach (Aula a in aulas)
                        {
                            //if(a.UserName.Equals(currentUser) && a.AulaAceite)
                            //{
                                Console.WriteLine($"{a.NumeroDoPedido} - {a.DataAula} - {a.PersonalTrainerName}");
                            //}
                        }*/

                    } break;

                default:
                    {
                        Utilitarios.AjudaInfo();
                    }
                    break;
            }
        }

        internal static void Sair()
        {
            exitFlag = false;
        }


        internal static void LimparConsola()
        {
            Console.Clear();
        }


        internal static void VerPedido()
        { }

        internal static void ListarPedidos()
        { }

        internal static void IntroduzirPedido(string nome, DateTime data)
        {
            //Aula a = new Aula();
            /*
            if(currentUser.Equals("RSGym"))
            {
                Console.WriteLine("Utilizador não autorizado a realizar a operação");
                Console.WriteLine("Por favor efetuar login na consola\n");
                return;
            }

            List<DateTime> tempList = new List<DateTime>();

            foreach (KeyValuePair<string, List<DateTime>> item in personalTrainers)
            {
                if(item.Key.Equals(nome))
                {
                    
                    foreach (DateTime d in item.Value)
                    {
                        if (data.Equals(d))
                        {
                            Console.WriteLine($"{item.Key} com este horario ocupado. Por favor selecionar outro dia e hora\n");
                            return;
                        }
                        else
                        {
                            if (d.Hour < 9 || d.Hour > 20)
                            {
                                Console.WriteLine("Marcação fora do horario permitido");
                                Console.WriteLine("Por favor introduzir um horario entre as 9:00 e as 20:00\n");
                                return;
                            }
                        
                            else
                            {

                                tempList = item.Value;

                                personalTrainers.Remove(nome);
                                tempList.Add(data);
                                personalTrainers.Add(nome, tempList);


                                Console.WriteLine($"Aula marcada com sucesso\n");
                                return;
                            }   
                        }
                    }
                }

            }

            Console.WriteLine($"O personal trainer com o nome {nome} não existe\n");
        }*/


        }
    }
}
