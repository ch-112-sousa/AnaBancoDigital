
namespace ContaCorrenteAPI.Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; set; }
        public long Numero { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Senha { get; set; }
        public string Salt { get;  set; 


        public ContaCorrente(long numero, string nome, bool ativo, string senha, string salt)
        {
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
            Senha = senha;
            Salt = salt;
        }
    }
}