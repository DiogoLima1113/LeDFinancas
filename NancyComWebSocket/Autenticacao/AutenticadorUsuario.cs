using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NancyComWebSocket.Dominio.Entidades;
using NancyComWebSocket.Dominio.Repositorio;
using System.Security.Claims;

namespace NancyComWebSocket.Autenticacao
{   
    // TODO: Remover padrão singleton e adicionar logs
    public class AutenticadorUsuario : IAutenticador, IUserMapper
    {
        private static AutenticadorUsuario instancia;
        private IList<Sessao> _sessoes = new List<Sessao>();

        public event Action<Sessao> OnNovaSessaoInserida = delegate {};

        private AutenticadorUsuario() { }

        public static AutenticadorUsuario GetInstance()
        {
            if (instancia == null)
                instancia = new AutenticadorUsuario();

            return instancia;
        }

        public ClaimsPrincipal GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            Sessao sessao = null;
            try
            {
                sessao = this.Obter(identifier);
            }
            catch (AutenticacaoException)
            {
                return null;
            }

            return new ClaimsPrincipal(sessao.Usuario);
        }
        

        public Sessao Obter(Guid guid)
        {
            var sessao = _sessoes.Where(s => s.Usuario.Guid.Equals(guid)).FirstOrDefault();
            if (sessao == null)
                throw new AutenticacaoException("Usuário não autenticado.");

            if (SessaoExpirada(sessao))
            {
                Remover(sessao);
                throw new AutenticacaoException("Usuário com sessão expirada.");
            }

            return sessao;
        }

        public IList<Sessao> Obter()
        {
            return _sessoes;
        }

        public Guid InserirSessao(Usuario usuario, DateTime expires)
        {
            var sessao = new Sessao(Guid.NewGuid())
            {
                Expires = expires,
                Usuario = usuario
            };

            usuario.Guid = sessao.Guid;
            _sessoes.Add(sessao);

            OnNovaSessaoInserida(sessao);
            
            return sessao.Guid;
        }

        public void Remover(Sessao sessao)
        {
            try
            {
                _sessoes.Remove(sessao);
            }
            catch (AutenticacaoException)
            {
                // Sessão não existe, logo, não é necessário fazer nada.
            }
        }

        public bool SessaoExpirada(Sessao sessao)
        {
            return (sessao.Expires <= DateTime.Now);
        }

        public void RemoverSessoesExpiradas()
        {
            var sessoesExpiradas = _sessoes.Where(s => s.Expires <= DateTime.Now);

            foreach (var sessao in sessoesExpiradas)
            {
                Remover(sessao);
            }
        }
    }
}
