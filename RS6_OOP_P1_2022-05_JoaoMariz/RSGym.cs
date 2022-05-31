using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        internal static string currentUser = "RSGym";


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
                            "\t\t Utilização: request -n {request} -d {dia} -h {horas}"},
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

        internal static List<Aula> aulas = new List<Aula>();


        private static Dictionary<string, List<DateTime>> personalTrainers = new Dictionary<string, List<DateTime>>()
        {
            { "Maria", new List<DateTime>() },
            { "José", new List<DateTime>() },
            { "MeninoJesus", new List<DateTime>() }
        };

        // Variavel com os requests feitos
        // Quando é introduzido um request, incrementa a key e grava o utilizador que o fez
        private static Dictionary<int, string> requests = new Dictionary<int, string>();

        // Metodo para lista o menu de ajuda com recurso ao Dictionary que é váriavel da Classe
        // usado o .Length para ficar tudo alinhado de acordo com a dimensão da primeira palavra
        // Se necessário adicionar mais item's, já fica formatado
        internal static void Ajuda() 
        {            
            foreach (KeyValuePair<string,string> item in helpMenu)
            {
                if(item.Key.Length < 8)
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
                        if (tmp[1] == "-u" && tmp[3] == "-p" && tmp.Length == 5)
                        {
                            Autenticacao.Login(tmp[2], tmp[4]);
                            IniciarConsola();
                        }
                        else
                            Utilitarios.AjudaInfo();
                    }
                    break;

                case "request":
                    {
                        if (tmp[1] == "-n" && tmp[3] == "-d" && tmp[5] == "-h" && tmp.Length == 7)
                        {
                            DateTime dia, hora, all = new DateTime();
                            string n = tmp[2];
                            bool d = DateTime.TryParse(tmp[4], out dia);
                            if (!d)
                            {
                                Console.WriteLine("Dia introduzido no formato errado. Use \"help\" na consola para obter ajuda\n");
                                break;
                            }
                            bool h = DateTime.TryParse(tmp[6], out hora);
                            if (!h)
                            {
                                Console.WriteLine("Hora introduzida no formato errado. Use \"help\" na consola para obter ajuda\n");
                                break;
                            }

                            all = dia.Date.Add(hora.TimeOfDay);

                            IntroduzirPedido(n, all);
                            return;
                        }
                        else
                            Utilitarios.AjudaInfo();
                    }
                    break;

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
            if(currentUser == "RSGym")
            {
                Console.WriteLine("Utilizador não autorizado a realizar a operação");
                Console.WriteLine("Por favor efetuar login na consola\n");
                return;
            }

            List<DateTime> tempList = new List<DateTime>();

            foreach (KeyValuePair<string, List<DateTime>> item in personalTrainers)
            {
                if(item.Key == nome)
                {
                    foreach (DateTime d in item.Value)
                    {
                        if (data == d)
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
                                int num = requests.Count;
                                tempList = item.Value;

                                personalTrainers.Remove(nome);
                                tempList.Add(data);
                                personalTrainers.Add(nome, tempList);
                                requests.Add(num + 1, currentUser);

                                Console.WriteLine($"Aula marcada com sucesso\n");
                                return;
                            }   
                        }
                    }
                }

            }

            Console.WriteLine($"O personal trainer com o nome {nome} não existe\n");
        }


    }
}
