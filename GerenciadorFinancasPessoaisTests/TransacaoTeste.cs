using GerenciadorFinancasPessoais;
using GerenciadorFinancasPessoais.Models;

namespace GerenciadorFinancasPessoaisTests
{
    public class TransacaoTeste
    {
        [Fact]
        public void TestaQuantiaMinimaDeTransacao()
        {
            // Arrange
            var transacao = new Transacao();

            // Act & Assert
            var exception = Assert.Throws<FormatException>(() => transacao.Valor = 0);

            // Assert
            Assert.Equal("A quantia deve ser no m�nimo de 0,01 centavos", exception.Message);
        }

        [Fact]
        public void TestaQuantiaMaximaDeTransacao()
        {
            var transacao = new Transacao();

            var excecao = Assert.Throws<FormatException>(() => transacao.Valor >= 1000000);

            Assert.Equal("O sistema n�o permite transa��es com um valor acima de 1 milh�o", excecao.Message);
        }
    }
}