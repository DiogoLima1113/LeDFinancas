using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NancyComWebSocket.Dominio.Entidades;

namespace NancyComWebSocket.Autenticacao
{
    public class Sessao
    {
        public Guid Guid { get; private set; }
        public Usuario Usuario { get; set; }
        public DateTime Expires { get; set; }

        public Sessao(Guid guid)
        {
            this.Guid = guid;
        }

        public override bool Equals(object obj)
        {
            var sessaoTemp = obj as Sessao;
            if (sessaoTemp == null)
                return false;


            return (sessaoTemp.Usuario.Equals(this.Usuario) && sessaoTemp.Guid == this.Guid && sessaoTemp.Expires == this.Expires);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }
    }
}
