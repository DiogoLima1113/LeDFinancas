namespace NancyComWebSocket.Dominio.Entidades
{
    public class NaturezaLancamento
    {
        public long Id {get;set;}
        public string Descricao {get;set;}
        public NaturezaLancamento(){}

        public NaturezaLancamento(long id, string descricao)
        {
            this.Id = id;
            this.Descricao = descricao;
        }
    }
}