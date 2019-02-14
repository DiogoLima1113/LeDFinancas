using System.Collections.Generic;
using Dapper;
using NancyComWebSocket.Dominio.Entidades;
using NancyComWebSocket.Dominio.Repositorios.Interfaces;
using System.Linq;


namespace NancyComWebSocket.Dominio.Repositorios.Classes
{
    public class DBRepositorioNaturezaLancamento : IRepositorioNaturezaLancamento
    {
        private IConnectionProvider Conn;

        public DBRepositorioNaturezaLancamento(IConnectionProvider conn)
        {
            Conn = conn;
        }
        public void Atualizar(NaturezaLancamento natureza)
        {
            using (var con = Conn.CreateNewConnection())
            {
               con.Query<NaturezaLancamento>(@"UPDATE natureza_lancamento
                                                    SET descricao = @Descricao
                                                    WHERE id = @Id ", new { Id = natureza.Id,
                                                                            Descricao = natureza.Descricao })
                                            .FirstOrDefault();
            }
        }

        // public void Deletar(NaturezaLancamento natureza)
        // {
        //     throw new System.NotImplementedException();
        // }

        public void Inserir(NaturezaLancamento natureza)
        {
            throw new System.NotImplementedException();
        }

        public NaturezaLancamento Obter(long id)
        {
            using (var con = Conn.CreateNewConnection())
            {
               return con.Query<NaturezaLancamento>(@"select id, descricao 
                                                    from natureza_lancamento 
                                                    where id = @Id ", new { Id = id}).FirstOrDefault();
            }
        }

        public IEnumerable<NaturezaLancamento> Todos()
        {
            using (var con = Conn.CreateNewConnection())
            {
               return con.Query<NaturezaLancamento>(@"select id, descricao from natureza_lancamento");
            }
        }
    }
}