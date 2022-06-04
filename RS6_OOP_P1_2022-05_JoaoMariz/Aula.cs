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


        internal static void Request(string argRequest)
        {
            string patternRequest = @"^request -n (?<nome>[A-zÀ-ú0-9]+) -d (?<data>[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]) -h (?<hora>[0-9][0-9]:[0-9][0-9]$)";

            if (Regex.IsMatch(argRequest, patternRequest, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternRequest, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(argRequest);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;

                    string[] d = groups["data"].Value.Split('/');
                    string[] h = groups["hora"].Value.Split(':');
                    string n = groups["nome"].Value;
                    int i = RSGym.aulaNumero;
                    string ptname = groups["nome"].Value;
                    
                    try
                    {
                        DateTime t = new DateTime(Convert.ToInt32(d[2]), Convert.ToInt32(d[1]), Convert.ToInt32(d[0]), Convert.ToInt32(h[0]), Convert.ToInt32(h[1]), 0);
                        Aula a = new Aula(ptname, RSGym.currentUser, t, Utilitarios.RandomizarAulaAceite(), i);

                        var ptCheck = RSGym.personalTrainers
                                        .Where(c => c.Nome.Equals(n));

                        if(!ptCheck.Any())
                        {
                            Console.WriteLine($"Lamentamos mas o PT com nome {n} não existe!\n");
                            return;
                        }

                        if (a.AulaAceite == true)
                        {
                            Utilitarios.ImprimirAula(a);
                            RSGym.aulas.Add(i, a);
                            RSGym.aulasTeste.Add(a);
                            RSGym.aulaNumero++;
                            Console.WriteLine("Aula introduzida e aceite pelo ginásio");
                            return;
                        }

                        else
                        {   
                            Console.WriteLine("Lamentamos mas o ginasio não aceitou o seu pedido");
                            return;
                        }
                    }

                    catch (System.ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("A data ou hora introduzidas são incorrectas");
                        return;
                    }
                }
            }

            else
            {
                Utilitarios.AjudaInfo();
            }
        }

        internal static void Cancel(string argCancel)
        {
            int pedido = 0;

            string patternRequest = @"^cancel -r (?<pedido>[0-9]+)$";

            if (Regex.IsMatch(argCancel, patternRequest, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternRequest, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(argCancel);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);

                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Key == pedido && aula.Value.UserName.Equals(RSGym.currentUser))
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

        internal static void Finish(string argFinish)
        {
            int pedido = 0;

            string patternFinish = @"^finish -r (?<pedido>[0-9]+)$";

            if (Regex.IsMatch(argFinish, patternFinish, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternFinish, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(argFinish);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);

                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Key == pedido && aula.Value.UserName.Equals(RSGym.currentUser) && aula.Value.Mensagem.Equals(string.Empty))
                            {
                                aula.Value.Mensagem = $"Aula concluída - {DateTime.Now}";
                                Console.WriteLine($"A sua aula foi concluida em {aula.Value.Mensagem}\n");
                                return;
                            }

                            if (aula.Key == pedido && aula.Value.UserName.Equals(RSGym.currentUser) && !aula.Value.Mensagem.Equals(string.Empty))
                            {
                                Console.WriteLine($"A sua aula foi já concluida com a mensagem: {aula.Value.Mensagem}\n");
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

        internal static void MyRequest(string argMyrequest)
        {
            int pedido = 0;

            string patternMyRequest = @"^myrequest -r (?<pedido>[0-9]+)$";

            if (Regex.IsMatch(argMyrequest, patternMyRequest, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternMyRequest, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(argMyrequest);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);

                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Key == pedido && aula.Value.UserName.Equals(RSGym.currentUser))
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

        internal static void Message(string argMessage) 
        {
            int pedido = 0;

            string patternMessage = @"^message -r (?<pedido>[0-9]+) -s (?<mensagem>[A-zÀ-ú0-9 ]+)";

            if (Regex.IsMatch(argMessage, patternMessage, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternMessage, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(argMessage);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);

                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Value.NumeroDoPedido == pedido && aula.Value.Mensagem.Equals(string.Empty))
                            {

                                aula.Value.Mensagem = $"{groups["mensagem"].Value} - {DateTime.Now}";
                                Console.WriteLine(aula.Value.Mensagem);
                                return;
                            }
                            
                            if (aula.Value.NumeroDoPedido == pedido && !aula.Value.Mensagem.Equals(string.Empty))
                            {
                                Console.WriteLine($"A aula numero {pedido} já se encontra concluida");
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
    
        internal static void Requests(string argRequests)
        {
            string patternRequests = @"^requests -a+$";

            if (Regex.IsMatch(argRequests, patternRequests, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternRequests, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(argRequests);

                foreach (Match match in matches)
                {
                    var minhasAulas = RSGym.aulas
                                        .Where(c => c.Value.UserName.Equals(RSGym.currentUser) && c.Value.AulaAceite)
                                        .OrderBy(c => c.Key);
                   
                    if (minhasAulas.Count() == 0)
                        Console.WriteLine("Não tem pedidos registados\n");

                    else
                    {
                        foreach (var i in minhasAulas)
                        {
                            Utilitarios.ImprimirAula(i.Value);
                        }
                    }
                }
                return;

            }

            else
            {
                Utilitarios.AjudaInfo();
            }
        }
    }
}
