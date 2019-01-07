using System.Data;
using System.Configuration;
using System;
using Microsoft.Extensions.Configuration;
using NancyComWebSocket.Dominio.Repositorios.Interfaces;
using System.Data.SqlClient;
using NancyComWebSocket.Dominio.Entidades;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace NancyComWebSocket.Dominio.Repositorio
{
    public class DBRepositorioUsuarios : IRepositorioUsuarios
    {
        private IConnectionProvider Conn;
        public DBRepositorioUsuarios(IConnectionProvider conn)
        {
            this.Conn = conn;
        }

        public void Inativar(Usuario usuario)
        {
            using (var con = Conn.CreateNewConnection())
            {
                con.Query(@"UPDATE usuarios
                            SET data_inativacao = SYSDATETIME()
                            WHERE id = @Id",
                            new {Id = usuario.Id});
            }
        }

        public Usuario Obter(long id)
        {
            using (var con = Conn.CreateNewConnection())
            {
                return con.Query<Usuario>("SELECT * FROM usuarios WHERE id = @Id",
                                         new {Id = id}).FirstOrDefault();
            }
        }

        public Usuario Obter(string login)
        {
            using (var con = Conn.CreateNewConnection())
            {
                return con.Query<Usuario>("SELECT * FROM usuarios WHERE login = @Login",
                                         new {Login = login}).FirstOrDefault();
            }
        }

        public string ObterSenha(long id)
        {
            using (var con = Conn.CreateNewConnection())
            {
                return con.Query<string>("SELECT senha FROM usuarios WHERE id = @Id ",
                new {Id = id}).FirstOrDefault();
            }
        }

        public IEnumerable<Usuario> Todos()
        {
            using (var con = Conn.CreateNewConnection())
            {
                return con.Query<Usuario>("SELECT * FROM usuarios");
            }
        }
    }
}