using System;
using System.Collections.Generic;
using NancyComWebSocket.Dominio.Entidades;
using NancyComWebSocket.Dominio.Repositorios.Interfaces;
using Dapper;
using System.Linq;

namespace NancyComWebSocket.Dominio.Repositorios.Classes
{
    public class DBRepositorioTitulos : IRepositorioTitulos
    {
        private IConnectionProvider Conn;
        public DBRepositorioTitulos(IConnectionProvider conn)
        {
            this.Conn = conn;
        }
        public void Adicionar(Titulo titulo)
        {
            using (var con = Conn.CreateNewConnection()){
                con.Query(@"INSERT INTO titulos VALUES
                            (@UsuarioId, @Numero, '@Descricao', '@Tipo', '@DataReferencia', '@DataVencimento',
                             SYSDATETIME(), @Valor, @NaturezaLancamentoId, @Observacao", 
                             new {UsuarioId = titulo.Id,
                                    Numero = titulo.Numero,
                                    Descricao = titulo.Descricao,
                                    Tipo = titulo.Tipo,
                                    DataReferencia = titulo.DataReferencia,
                                    DataVencimento = titulo.DataVencimento,
                                    Valor = titulo.Valor,
                                    NaturezaLancamentoId = titulo.NaturezaLancamentoId,
                                    Observacao = titulo.Observacao});
            }
        }

        public void Atualizar(Titulo titulo)
        {
            using(var con = Conn.CreateNewConnection()){
                con.Query(@"UPDATE titulos
                            SET usuario_id = @UsuarioId, numero = '@Numero', tipo = '@Tipo',
                             data_referencia = '@data_referencia', data_vencimento = '@DataVencimento',
                             valor = @Valor, natureza_lancamento_id = @NaturezaLancamentoId, 
                             observacao = '@Observacao'
                            WHERE id = @Id",
                            new{UsuarioId = titulo.UsuarioId,
                                Numero = titulo.Numero,
                                Tipo = titulo.Tipo,
                                DataReferencia = titulo.DataReferencia,
                                DataVencimento = titulo.DataVencimento,
                                Valor = titulo.Valor,
                                NaturezaLancamentoId = titulo.NaturezaLancamentoId,
                                Observacao = titulo.Observacao,
                                Id = titulo.Id});
            }
        }

        public void Inativar(Titulo titulo)
        {
            using (var con = Conn.CreateNewConnection())
            {
                con.Query(@"UPDATE titulos
                            SET data_inativacao = SYSDATETIME()
                            WHERE id = @Id",
                            new {Id = titulo.Id});
            }
        }

        public Titulo Obter(long id)
        {
            using (var con = Conn.CreateNewConnection())
            {
                return con.Query<Titulo>("SELECT * FROM titulos WHERE id = @Id",
                                         new {Id = id}).FirstOrDefault();
            }
        }

        public IEnumerable<Titulo> ObterCadastro(DateTime dataInicio, DateTime dataFim)
        {
            using(var con = Conn.CreateNewConnection()){
                return con.Query<Titulo>("SELECT * FROM titulos WHERE data_cadastro between '@DataInicio' and '@DataFim'",
                                        new{DataInicio = dataInicio,
                                            DataFim = dataFim});
            }
        }

        public IEnumerable<Titulo> ObterReferencia(DateTime dataInicio, DateTime dataFim)
        {
            using(var con = Conn.CreateNewConnection()){
                return con.Query<Titulo>("SELECT * FROM titulos WHERE data_referencia between '@DataInicio' and '@DataFim'",
                                        new{DataInicio = dataInicio,
                                            DataFim = dataFim});
            }
        }

        public IEnumerable<Titulo> ObterVencimento(DateTime dataInicio, DateTime dataFim)
        {
            using(var con = Conn.CreateNewConnection()){
                return con.Query<Titulo>("SELECT * FROM titulos WHERE data_vencimento between '@DataInicio' and '@DataFim'",
                                        new{DataInicio = dataInicio,
                                            DataFim = dataFim});
            }
        }

        public IEnumerable<Titulo> Todos()
        {
            var sql = @"SELECT 
                        id, usuario_id AS usuarioId, 
                        numero, descricao,
                        tipo, data_referencia as datareferencia,
                        data_vencimento as datavencimento,
                        data_cadastro as datacadastro, valor,
                        natureza_lancamento_id as naturezalancamentoid,
                        observacao, data_inativacao as datainativacao 
                        FROM titulos";

            using(var con = Conn.CreateNewConnection()){
                return con.Query<Titulo>(sql);
            }
        }
    }
}