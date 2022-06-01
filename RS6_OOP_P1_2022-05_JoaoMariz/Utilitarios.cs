using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    internal class Utilitarios
    {
        /* 
        * Variavel helpMenu com a informação de todos os comandos.
        * Usada a flag "readonly" dado que não é para alterar o conteudo.
        * Apresenta o menu completo após o utilizador realizar a operação de login
        */

        internal static readonly Dictionary<string, string> helpMenu = new Dictionary<string, string>
        {
            {"help" ,       "Lista este menu de ajuda"},
            {"exit" ,       "Sair da aplicação"},
            {"clear",       "Limpa a consola"},
            {"login",       "Fazer login na aplicação para ter acesso ao menu completo e registar pedidos.\n" +
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


        internal static readonly Dictionary<string, string> helpMenuNotLogged = new Dictionary<string, string>
        {
            {"help" ,       "Lista este menu de ajuda"},
            {"exit" ,       "Sair da aplicação"},
            {"clear",       "Limpa a consola"},
            {"login",       "Fazer login na aplicação para ter acesso ao menu completo e registar pedidos.\n" +
                            "\t\t Utilização: login -u {username} -p {password}"}
        };

        internal static void AjudaInfo()
        {
            Console.WriteLine("Comando não reconhecido. Use \"help\" para obter ajuda\n");
        }

        internal static bool RandomizarAulaAceite()
        {
            Random r = new Random();
            int n = r.Next(0,2);

            if (n == 1)
                return true;
            else
                return false;
        }

        internal static void ImprimirAula(Aula a)
        {
            Console.WriteLine("-- Detalhe da aula --");
            Console.WriteLine($"\tNumero do pedido: {a.NumeroDoPedido}");
            Console.WriteLine($"\tNome do PT: {a.PersonalTrainerName}");
            Console.WriteLine($"\tHora da Aula: {a.DataDaAula.Hour}");
            Console.WriteLine($"\tData da Aula: {a.DataDaAula.Date}");
            Console.WriteLine($"\tMensagem: {a.Mensagem}");
        }
    }
}