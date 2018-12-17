using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyComWebSocket.Autenticacao
{
    public class AutenticacaoException : Exception
    {
        public AutenticacaoException(string message) : base(message) { }
    }
}
