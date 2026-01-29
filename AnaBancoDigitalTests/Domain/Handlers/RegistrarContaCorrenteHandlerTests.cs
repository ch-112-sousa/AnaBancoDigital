using ContaCorrenteAPI.Domain.Command.Requests;
using ContaCorrenteAPI.Domain.Entities;
using ContaCorrenteAPI.Domain.Handlers;
using ContaCorrenteAPI.Repositories;
using Moq;

namespace AnaBancoDigitalTests.Domain.Handlers
{
    public class RegistrarContaCorrenteHandlerTests
    {

        [Fact]
        public async Task Handle_DeveRegistrarNovaContaCorrente_ComSucesso()
        {
            var _contaCorrenteRepositoryMock = new Mock<IContaCorrenteRepository>();
            var _usuarioRepositoryMock = new Mock<IUsuarioRepository>();

            var c = new ContaCorrente();
            c.IdContaCorrente = "CC A1";
            c.Ativo = true;
            c.Numero = 2221;
            c.Nome = "Carlos Sousa";
            c.Senha = "1234";
            c.Salt = "salt";

            _contaCorrenteRepositoryMock.Setup(repo => repo.SalvarRegistroAsync(It.IsAny<ContaCorrente>()))
                .Returns(Task.FromResult(true));

            var handler = new RegistrarContaCorrenteHandler(_contaCorrenteRepositoryMock.Object);
            var command = new RegistrarContaCorrenteRequest() 
            { 
                Senha = c.Senha,
                Numero = c.Numero,
                Nome = c.Nome,
                Salt = c.Salt            
            };

            var res = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(res);
            _contaCorrenteRepositoryMock.Verify(repo => repo.SalvarRegistroAsync(It.IsAny<ContaCorrente>()), Times.Once);
        }
    }
}
