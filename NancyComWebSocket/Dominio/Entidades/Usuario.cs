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
        public DateTime? DataInativacao {get;set;}
        public DateTime DataCadastro {get;set;}
        public long PerfilId {get;set;}
            
        public IEnumerable<string> Claims { get; set; }

        public string AuthenticationType => "Default";

        public bool IsAuthenticated => this.Guid != null;

        public string Login => Name;

        // Isso aqui é para atender uma propriedadE que a InterFace  IIdentity tem.
        // Remova e veja a interface vai pedir para você implementar um Name que só tenha retorno.
        public string Name 
        { 
                get
                {
                    return Name;
                }
        }


        // Alterando nosso ToString().
        public override string ToString()
        {
            return string.Format("{0} | {1} | {3} ", Id, Guid, Name);
        }

        //Gera um Hash baseado no nome.
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
