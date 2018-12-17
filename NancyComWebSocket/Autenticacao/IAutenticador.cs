using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NancyComWebSocket.Dominio.Entidades;

namespace NancyComWebSocket.Autenticacao
{
    public interface IAutenticador
    {
        Sessao Obter(Guid guid);
        IList<Sessao> Obter();
        Guid InserirSessao(Usuario usuario, DateTime expires);
        void Remover(Sessao sessao);
        bool SessaoExpirada(Sessao sessao);
        void RemoverSessoesExpiradas();
        event Action<Sessao> OnNovaSessaoInserida;
    }
}
