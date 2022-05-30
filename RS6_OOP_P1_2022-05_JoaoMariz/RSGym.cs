using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    class RSGym
    {
        #region Constructores
        internal string PersonalTrainerName { get; set; }
        internal string UserName { get; set; }
        internal DateTime DataDaAula { get; set; }
        internal bool LogInCorrecto { get; set; }
        internal bool AulaAceite { get; set; }
        internal int NumeroDoPedido { get; set; }

        // Construtor vazio
        internal RSGym()
        {
            string PersonalTrainerName = string.Empty;
            string UserName = string.Empty;
            DateTime DataDaAula = DateTime.MinValue;
            bool LogInCorrecto = false;
            bool AulaAceite = false;
            int NumeroDoPedido = 0;
        }

        // Construtor completo
        internal RSGym(string pt, string un, DateTime d, bool log, bool aa, int num)
        {
            string PersonalTrainerName = pt;
            string UserName = un;
            DateTime DataDaAula = d;
            bool LogInCorrecto = log;
            bool AulaAceite = aa;
            int NumeroDoPedido = num;
        }

        #endregion

        // Variavel onde vai ser "guardado" o utilizador que se encontra ativo
        internal static string currentUser = "RSGym";
        internal static bool exitFlag = true;

        // Variavel helpMenu.
        // Serve tambem para verificar se a opção introduzida é correcta
        private static Dictionary<string, string> helpMenu = new Dictionary<string, string>
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

        // Lista com os nomes dos PT's e Datas registadas entretanto
        private static Dictionary<string, List<DateTime>> personalTrainers = new Dictionary<string, List<DateTime>>()
        {
            { "Maria", new List<DateTime>() { DateTime.MinValue } },
            { "José", new List<DateTime>() { DateTime.MinValue } },
            { "MeninoJesus", new List<DateTime>() { DateTime.MinValue } }
        };

        // Variavel com os requests feitos
        // Quando é introduzido um request, incrementa a key e grava o utilizador que o fez
        private static Dictionary<int, string> requests = new Dictionary<int, string>();

        // Metodo para lista o menu de ajuda com recurso ao Dictionary que é váriavel da Classe
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
            Console.Write($"{currentUser} >");
            arg = Console.ReadLine();
            ParseArgument(arg);
        }

        private static void ParseArgument(string arg)
        {

        }

        internal static void Sair()
        {
            exitFlag = false;
        }

        internal static void LimparConsola()
        {
            Console.Clear();
        }

        internal static void Request() // ver se da para usar o outro metodo
        { }

        internal static void AnularAula()
        { }

        internal static void ConcluirAula()
        { }

        internal static void InserirMensagem()
        { }

        internal static void VerPedido()
        { }

        internal static void ListarPedidos()
        { }

        internal void IntroduzirPedido(string nome, DateTime data)
        {
            List<DateTime> tempList = new List<DateTime>();

            foreach (KeyValuePair<string, List<DateTime>> item in personalTrainers)
            {
                if(item.Key.Contains(nome))
                {
                    foreach (DateTime d in item.Value)
                    {
                        if (data == d)
                        {
                            Console.WriteLine($"{item.Key} com este horario ocupado. Por favor selecionar outro dia e hora");
                            Console.WriteLine("Pressionar qualquer tecla para continuar");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            if (d.Hour < 9 || d.Hour > 20)
                            {
                                Console.WriteLine("Marcação fora do horario permitido");
                                Console.WriteLine("Pressionar qualquer tecla para continuar");
                                Console.ReadKey();
                                break;
                            }
                        
                            else
                            {
                                tempList = item.Value;
                                personalTrainers.Remove(nome);
                                tempList.Add(data);
                                personalTrainers.Add(nome, tempList);
                                Console.WriteLine($"Aula marcada com sucesso");
                                Console.WriteLine("Pressionar qualquer tecla para continuar");
                                Console.ReadKey();
                            }   
                        }
                    }
                }

                else 
                {
                    Console.WriteLine($"O personal trainer com o nome {nome} não existe");
                    Console.WriteLine("Pressionar qualquer tecla para continuar");
                    Console.ReadKey();
                }
            }
        }


    }
}
