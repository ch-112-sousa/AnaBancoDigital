namespace ContaCorrenteAPI.Domain.Command.Responses
{
    [Serializable]
    public class RegistrarContaCorrenteResponse  
    {
        public long Numero { get; set;  }
        public string Nome { get; set; }

        public string Error { get; set; } 
    }
}