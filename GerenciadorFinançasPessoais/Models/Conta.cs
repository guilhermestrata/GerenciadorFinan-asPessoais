using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorFinancasPessoais.Models
{
    public class Conta
    {
        public Conta()
        {
            transacoes = new List<Transacao>();
        }
        private List<Transacao> transacoes;

        public List<Transacao> Transacoes { get =>  transacoes; set => transacoes = value; }

        public void RegistrarTransacao(Transacao transacao)
        {
            transacao.Data = DateTime.Now;
            transacao.Comprovante = GeraComprovante(transacao);
            Transacoes.Add(transacao);
        }

        public Transacao BuscaTransacao(string codigo)
        {
            var encontrado = (from transacao in Transacoes
                              where transacao.Codigo == codigo
                              select transacao).SingleOrDefault();

            return encontrado;
        }

        public string GeraComprovante(Transacao transacao)
        {
            string identificador = new Guid().ToString().Substring(0, 5);
            transacao.Codigo = identificador;

            string comprovante = "####  COMPROVANTE DE TRANSAÇÃO ####\n" +
                                 $"Codigo: {identificador}" +
                                 $"Data de realização: {DateTime.Now}" +
                                 $"Realizada por: {transacao.Nome}";

            return comprovante;
        }
    }
}
