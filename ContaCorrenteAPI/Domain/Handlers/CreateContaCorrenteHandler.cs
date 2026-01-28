using ContaCorrenteAPI.Domain.Command.Requests;
using ContaCorrenteAPI.Domain.Command.Responses;
using ContaCorrenteAPI.Domain.Entities;

namespace ContaCorrenteAPI.Domain.Handlers
{
    public class CreateContaCorrenteHandler
    {
        ICustomerRepository _repository;
        IEmailService _emailService;

        public CreateCustomerHandler(ICustomerRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public CreateContaCorrenteResponse Handle(CreateContaCorrenteRequest command)
        {
            
            var customer = new ContaCorrente(command.Numero, command.Nome, true, command.Senha, command.Salt);

            _repository.Save(customer);
            
            return new CreateContaCorrenteResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Date = DateTime.Now
            };
        }
    }
}
