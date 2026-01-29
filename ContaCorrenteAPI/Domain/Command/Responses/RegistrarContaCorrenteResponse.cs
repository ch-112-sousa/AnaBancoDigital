using ContaCorrenteAPI.Domain.Models;
using System.Text.Json.Serialization;

namespace ContaCorrenteAPI.Domain.Command.Responses
{
    [Serializable]
    public class RegistrarContaCorrenteResponse  
    {
        public long Numero { get; set;  }
        public string Nome { get; set; }

        [JsonIgnore]
        public ResultadoBase Info{ get; set; } 
    }
}