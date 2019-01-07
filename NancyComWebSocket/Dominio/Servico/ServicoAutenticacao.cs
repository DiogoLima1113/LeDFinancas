using System;
using System.Security.Cryptography;
using System.Text;
using NancyComWebSocket.Dominio.Repositorios.Interfaces;
using System.Linq;

namespace NancyComWebSocket.Dominio.Servico
{
    public class ServicoAutenticacao
    {
        private IRepositorioUsuarios RepositorioUsuario;
        private string Salt = "@LD";

        public ServicoAutenticacao(IRepositorioUsuarios repUsuario)
        {
            this.RepositorioUsuario = repUsuario;
        }

        public bool ObterUsuarioAutenticado(string login, string senha)
        {
            var pwd = Salt + senha;
            var senhaSalt = Base64Encode(pwd);
            var hash = GerarHashMd5(senhaSalt);
            var usuario = RepositorioUsuario.Obter(login);

            var senhaBanco = RepositorioUsuario.ObterSenha(usuario.Id);
            return (senhaBanco == hash);
        }

        private static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string GerarHashMd5(string senhaSalt)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senhaSalt));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        
    }
}