using System.Data;
using System.Configuration;
using System;
using Microsoft.Extensions.Configuration;
using NancyComWebSocket.Dominio.Repositorios.Interfaces;
using System.Data.SqlClient;

namespace NancyComWebSocket.Dominio.Repositorio
{
    public class ConnectionProvider : IConnectionProvider
    {
        private IConfiguration Configuration;
        private static string ConnectionString;

        // Metodo que constroi um  Provider e ja inicia um migration.
        // Não tem migraton neste projeto por enquanto.
        public ConnectionProvider(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = @"Data Source=DESKTOP-95IKDVP\SQLEXPRESS;
                                Initial Catalog=LDFinancas;
                                Integrated Security=SSPI";
        }

        public IDbConnection CreateNewConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public void CommitTransaction(IDbTransaction transaction)
        {
            try
            {
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
