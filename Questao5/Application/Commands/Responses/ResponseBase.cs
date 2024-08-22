namespace Questao5.Application.Commands.Responses
{
    public class ResponseBase
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string TipoErro { get; set; }
    }
}
