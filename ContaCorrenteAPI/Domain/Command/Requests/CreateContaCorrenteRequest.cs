namespace ContaCorrenteAPI.Domain.Command.Requests
{
    public class CreateContaCorrenteRequest
    {
        public string CPF { get; set; }
        public long Numero { get; set; }
        public string Nome { get; set; }        
        public string Senha { get; set; }
        public string Salt  { get; set; }
}
