using GerenciadorFinancasPessoais.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorFinancasTeste.Registro
{
    public class TransacoesRegistradasTeste
    {
        [Fact]
        public void RetornoDeNenhumaTransacaoRegistrada()
        {
            List<Transacao> transacoes = new List<Transacao>();

            int numeroDeTransacoes = transacoes.Count();

            Assert.Equal(0, numeroDeTransacoes);
        }
        [Fact]
        public void RetornoDeTransacaoRegistrada()
        {
            List<Transacao> transacoes = new List<Transacao>();
            var transacao = new Transacao();

            transacao.Tipo = TipoTransacao.Despesa;
            transacao.Valor = 1000;
            transacao.Descricao = "Obrigado";
            transacao.Data = DateTime.Now;

            transacoes.Add(transacao);

            Assert.Single(transacoes); //Verifica se há apenas um item na Lista transacoes

            Assert.Equal(TipoTransacao.Despesa, transacoes[0].Tipo); // Verifica se o tipo de transação foi configurado corretamente
            Assert.Equal(1000, transacoes[0].Valor); // Verifica se o valor da transação foi configurado corretamente
            Assert.Equal("Obrigado", transacoes[0].Descricao); // Verifica se a descrição da transação foi configurada corretamente
        }
    }
}
