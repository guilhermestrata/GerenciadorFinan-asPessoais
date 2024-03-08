using GerenciadorFinancasPessoais.Models;

namespace GerenciadorFinancasTeste.Exceptions
{
    public class ExceptionsTransacaoTeste
    {
        [Fact]
        public void TestaValorMinimoDeTransacao()
        {
            var transacao = new Transacao();

            var exception = Assert.Throws<FormatException>(() => transacao.Valor = 0);

            Assert.Equal("A quantia deve ser no mínimo de 0,01 centavos", exception.Message);
        }

        [Fact]
        public void TestaValorMaximoDeTransacao()
        {
            var transacao = new Transacao();

            var exception = Assert.Throws<FormatException>(() => transacao.Valor = 1000000000);

            Assert.Equal("O sistema não permite transações com um valor acima de 1 milhão", exception.Message);
        }
    }
}