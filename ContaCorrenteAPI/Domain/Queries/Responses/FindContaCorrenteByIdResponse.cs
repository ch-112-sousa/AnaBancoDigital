namespace ContaCorrenteAPI.Domain.Queries.Responses
{
    public class FindContaCorrenteByIdResponse
    {
        public string IdContaCorrente { get; set; }
        public long Numero { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
