using System;
using System.Text.RegularExpressions;
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
        internal string PersonalTrainerName { get; set; }
        internal string UserName { get; set; }
        internal DateTime DataDaAula { get; set; }
        internal bool AulaAceite { get; set; }
        internal int NumeroDoPedido { get; set; }
        internal string Mensagem { get; set; }

        internal Aula()
        {
            PersonalTrainerName = string.Empty;
            UserName = string.Empty;
            DataDaAula = new DateTime();
            AulaAceite = true;
            Mensagem = string.Empty;
            NumeroDoPedido = 0;
        }

        internal Aula(string pt, string un, DateTime d, bool aa, int num)
        {
            PersonalTrainerName = pt;
            UserName = un;
            DataDaAula = d;
            AulaAceite = aa;
            NumeroDoPedido = num;
            Mensagem = string.Empty;
        }

        internal Aula(string pt, string un, DateTime d, bool aa, int num, string msg)
        {
            PersonalTrainerName = pt;
            UserName = un;
            DateTime DataDaAula = d;
            AulaAceite = aa;
            NumeroDoPedido = num;
            Mensagem = msg;
        }

        internal static void Request(string arg)
        {
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
                    int i = RSGym.aulas.Keys.Last() + 1;
                    string ptname = groups["nome"].Value;

                    DateTime t = new DateTime(Convert.ToInt32(d[2]), Convert.ToInt32(d[1]), Convert.ToInt32(d[0]), Convert.ToInt32(h[0]), Convert.ToInt32(h[1]), 0);

                    Aula a = new Aula(ptname, RSGym.currentUser, t, Utilitarios.RandomizarAulaAceite(), i);

                    if (a.AulaAceite == true)
                    {
                        Utilitarios.ImprimirAula(a);
                        RSGym.aulas.Add(i, a);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Lamentamos mas o ginasio não aceitou o seu pedido");
                        return;
                    }
                }
            }

            else
            {
                Utilitarios.AjudaInfo();
            }
        }

        internal static void CancelarPedido(string a)
        {
            int pedido = 0;

            string patternRequest = @"^cancel -r (?<pedido>[0-9]+)";

            if (Regex.IsMatch(a, patternRequest, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternRequest, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(a);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);
                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Value.NumeroDoPedido == pedido)
                            {
                                RSGym.aulas.Remove(pedido);
                                Console.WriteLine($"O pedido numero {pedido} foi eliminado\n");
                                return;
                            }
                        }

                        Console.WriteLine("Pedido não encontrado\n");
                    }
                }

            }

            else
            {
                Utilitarios.AjudaInfo();
            }
        }

        internal static void ConcluirAula(string a)
        {
            int pedido = 0;

            string patternFinish = @"^finish -r (?<pedido>[0-9]+)";

            if (Regex.IsMatch(a, patternFinish, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternFinish, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(a);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);

                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Value.NumeroDoPedido == pedido)
                            {
                                aula.Value.Mensagem = $"Aula concluída {DateTime.Now}";
                                Console.WriteLine(aula.Value.Mensagem);
                                return;
                            }
                        }

                        Console.WriteLine("Pedido não encontrado");
                    }

                }
            }
            else
            {
                Utilitarios.AjudaInfo();
            }
        }

        internal static void MyRequest(string arg)
        {
            int pedido = 0;

            string patternMyRequest = @"^myrequest -r (?<pedido>[0-9]+)";

            if (Regex.IsMatch(arg, patternMyRequest, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternMyRequest, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(arg);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);

                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Value.NumeroDoPedido == pedido)
                            {
                                Utilitarios.ImprimirAula(aula.Value);
                                return;
                            }
                        }

                        Console.WriteLine("Pedido não encontrado");
                        return;
                    }
                }
            }

            else
            {
                Utilitarios.AjudaInfo();
            }
        }

        internal static void InserirMensagem(string s) 
        {
            int pedido = 0;

            string patternMessage = @"^message -r (?<pedido>[0-9]+) -s (?<mensagem>[A-zÀ-ú0-9]+)";

            if (Regex.IsMatch(s, patternMessage, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternMessage, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(s);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);

                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Value.NumeroDoPedido == pedido)
                            {
                                aula.Value.Mensagem = $"{groups["mensagem"].Value} - {DateTime.Now}";
                                Console.WriteLine(aula.Value.Mensagem);
                                return;
                            }
                        }

                        Console.WriteLine("Pedido não encontrado");
                        return;
                    }

                }
            }
            else
            {
                Utilitarios.AjudaInfo();
            }
        } 
    
    }
}
