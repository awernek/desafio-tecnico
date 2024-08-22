using Newtonsoft.Json.Converters;
using Questao5.Domain.Enumerators;
using System.Text.Json.Serialization;

namespace Questao5.Application.Commands.Responses
{
    public class CriarMovimentoResponse : ResponseBase
    {
        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public string DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public double Valor { get; set; }
    }
}
