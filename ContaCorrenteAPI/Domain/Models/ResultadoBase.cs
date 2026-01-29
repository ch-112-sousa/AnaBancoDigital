namespace ContaCorrenteAPI.Domain.Models
{
    public class ResultadoBase
    {
        public bool Successo { get; set; }
        public List<string> MensagensDeErro { get; set; }
        public List<string> MensagensDeErroValidacao { get; set; }

        public ResultadoBase()
        {
            MensagensDeErro = new List<string>();
        }
    }
}
