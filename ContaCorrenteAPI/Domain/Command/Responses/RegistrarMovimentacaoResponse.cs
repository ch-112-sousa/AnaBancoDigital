using ContaCorrenteAPI.Domain.Models;
using System.Text.Json.Serialization;

namespace ContaCorrenteAPI.Domain.Command.Responses
{
    public class RegistrarMovimentacaoResponse
    {
        [JsonIgnore]
        public ResultadoBase Info { get; set; }
    }
}
