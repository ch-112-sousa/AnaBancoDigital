namespace ContaCorrenteAPI.Domain.Queries.Responses
{
    public class BuscarContaCorrentePeloIdResponse
    {
        public string IdContaCorrente { get; set; }
        public long Numero { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
