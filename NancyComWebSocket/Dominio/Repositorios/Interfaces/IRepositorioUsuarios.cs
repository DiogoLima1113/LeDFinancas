using System.Collections.Generic;
using NancyComWebSocket.Dominio.Entidades;

namespace NancyComWebSocket.Dominio.Repositorios.Interfaces
{
    public interface IRepositorioUsuarios
    {
        Usuario Obter(long id);
        Usuario Obter(string login);
        IEnumerable<Usuario> Todos();
        void Inativar(Usuario usuario);
        string ObterSenha(long id);
    }
}