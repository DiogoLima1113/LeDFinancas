using System;
using System.Collections.Generic;
using NancyComWebSocket.Dominio.Entidades;

namespace NancyComWebSocket.Dominio.Repositorios.Interfaces
{
    public interface IRepositorioTitulos
    {
        void Adicionar(Titulo titulo);
        void Atualizar(Titulo titulo);
        void Inativar(Titulo titulo);
        IEnumerable<Titulo> Todos();
        Titulo Obter(long id);
        IEnumerable<Titulo> ObterReferencia(DateTime dataInicio, DateTime dataFim);
        IEnumerable<Titulo> ObterVencimento(DateTime dataInicio, DateTime dataFim);
        IEnumerable<Titulo> ObterCadastro(DateTime dataInicio, DateTime dataFim);
    }
}