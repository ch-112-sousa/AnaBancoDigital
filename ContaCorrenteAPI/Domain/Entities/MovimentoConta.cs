namespace ContaCorrenteAPI.Domain.Entities
{
    public class MovimentoConta
    {
        public string IdMovimento { get; set; }
        public string IdContaCorrente{ get; set; }
        public DateTime DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public float Valor { get; set; }
    }
}