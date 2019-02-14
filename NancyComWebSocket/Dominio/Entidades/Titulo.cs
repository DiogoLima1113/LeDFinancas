using System;

namespace NancyComWebSocket.Dominio.Entidades
{
    public class Titulo
    {
        public long Id{get; private set;}
        public long UsuarioId{get; set;}
        public string Numero{get; set;}
        public string Descricao{get; set;}
        public string Tipo{get; set;}
        public DateTime? DataReferencia{get; set;}
        public DateTime? DataVencimento{get; set;}
        public DateTime DataCadastro{get; set;}
        public DateTime? DataInativacao{get; set;}
        public double Valor{get; set;}
        public long NaturezaLancamentoId{get; set;}
        public string Observacao{get; set;}
        public Titulo(){}
        public Titulo(long id, long usuarioId, string numero, string descricao, string tipo, DateTime? dataReferencia,
                        DateTime? dataVencimento, DateTime dataCadastro, double valor, long naturezaLancamentoId, string observacao, DateTime? dataInativacao)
        {
            this.Id = id;
            this.UsuarioId = usuarioId;
            this.Numero = numero;
            this.Descricao = descricao;
            this.Tipo = tipo;
            this.DataReferencia = dataReferencia;
            this.DataVencimento = dataVencimento;
            this.DataCadastro = dataCadastro;
            this.Valor = valor;
            this.NaturezaLancamentoId = naturezaLancamentoId;
            this.Observacao = observacao;
            this.DataInativacao = dataInativacao;
        }
        public Titulo(long usuarioId, string numero, string descricao, string tipo, DateTime? dataReferencia,
                        DateTime? dataVencimento, DateTime dataCadastro, double valor, long naturezaLancamentoId, string observacao, DateTime? dataInativacao)
        {
            this.Id = 0;
            this.UsuarioId = usuarioId;
            this.Numero = numero;
            this.Descricao = descricao;
            this.Tipo = tipo;
            this.DataReferencia = dataReferencia;
            this.DataVencimento = dataVencimento;
            this.DataCadastro = dataCadastro;
            this.Valor = valor;
            this.NaturezaLancamentoId = naturezaLancamentoId;
            this.Observacao = observacao;
            this.DataInativacao = dataInativacao;
        }
    }
}