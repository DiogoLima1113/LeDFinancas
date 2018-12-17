using System.Data;

namespace NancyComWebSocket.Dominio.Repositorios.Interfaces
{
    public interface IConnectionProvider
    {
        IDbConnection CreateNewConnection();
        void CommitTransaction(IDbTransaction transaction);
    }
}
