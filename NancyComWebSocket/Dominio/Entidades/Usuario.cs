using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Nancy.Security;

namespace NancyComWebSocket.Dominio.Entidades
{
    public class Usuario : IIdentity
    {
        public long Id { get; private set; }
        public Guid Guid { get; set; }
        public string Nome {get; set;}
        public string Login {get;set;}
        public DateTime? DataInativacao {get;set;}
        public DateTime DataCadastro {get;set;}
        public long PerfilId {get;set;}
            
        public IEnumerable<string> Claims { get; set; }

        public string AuthenticationType => "Default";

        public bool IsAuthenticated => this.Guid != null;

        // public string Login => Name;

        // Isso aqui é para atender uma propriedadE que a InterFace  IIdentity tem.
        // Remova e veja a interface vai pedir para você implementar um Name que só tenha retorno.
        public string Name 
        { 
                get
                {
                    return Login;
                }
        }


        public Usuario(){}
        public Usuario(long id, string nome, Guid guid, string login,
                       DateTime dataCadastro, DateTime? dataInativacao, long perfilId)
        {
            this.Id = id;
            this.Nome = nome;
            this.Guid = guid;
            this.DataCadastro = dataCadastro;
            this.DataInativacao = dataInativacao;
            this.PerfilId = perfilId;
            this.Login = login;
        }
    }
}
