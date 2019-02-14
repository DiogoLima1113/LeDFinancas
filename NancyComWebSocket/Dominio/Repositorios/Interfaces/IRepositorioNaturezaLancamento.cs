using System.Collections.Generic;
using NancyComWebSocket.Dominio.Entidades;

namespace NancyComWebSocket.Dominio.Repositorios.Interfaces
{
    public interface IRepositorioNaturezaLancamento
    {
        void Inserir(NaturezaLancamento natureza);
        void Atualizar(NaturezaLancamento natureza);
        // void Deletar(NaturezaLancamento natureza);
        IEnumerable<NaturezaLancamento> Todos();
        NaturezaLancamento Obter(long id);
    }
}