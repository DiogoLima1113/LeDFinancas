using System.Data;
using System.Configuration;
using System;
using Npgsql;
using Microsoft.Extensions.Configuration;
using NancyComWebSocket.Dominio.Repositorios.Interfaces;

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
            ConnectionString = Configuration["ConnectionStrings:Database"];
            // string path = (new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            // path = System.IO.Path.GetDirectoryName(System.Uri.UnescapeDataString(path)) + System.IO.Path.DirectorySeparatorChar;
            // DatabaseInstaller.Install("postgres", ConnectionString, path + "Sitema.Migrations.dll");
        }

        public IDbConnection CreateNewConnection()
        {
            // Retorna uma conexão postgrees.
            // Fazer um semelhante para conexão SQL.
            return new NpgsqlConnection(ConnectionString);
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
