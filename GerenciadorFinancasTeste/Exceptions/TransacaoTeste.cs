using GerenciadorFinancasPessoais.Models;

namespace GerenciadorFinancasTeste.Exceptions
{
    public class TransacaoTeste
    {
        [Fact]
        public void TestaValorNegativoDeTransacao()
        {
            var transacao = new Transacao();

            Assert.Throws<ArgumentException>(() => transacao.Valor = -10);
        }

        [Fact]
        public void TestaValorMaximoDeTransacao()
        {
            var transacao = new Transacao();

            Assert.Throws<ArgumentException>(() => transacao.Valor = 11000);
        }
    }
}