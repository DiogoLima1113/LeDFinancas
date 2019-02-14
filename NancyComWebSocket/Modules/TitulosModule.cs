using Nancy;
using Nancy.Security;
using NancyComWebSocket.Dominio.Repositorios.Interfaces;
using Newtonsoft.Json;

namespace NancyComWebSocket.Modules
{
    public class TitulosModule : NancyModule
    {
        public TitulosModule(IRepositorioNaturezaLancamento repositorioNaturezaLancamento, 
                             IRepositorioUsuarios repositorioUsuarios,
                             IRepositorioTitulos repositorioTitulos)
        {
            this.RequiresAuthentication();

            Get("/dadosParaCadastro", _ =>
            {
                // Aqui vou fazer minha lógica para devolver os dados.
                // Vamos precisar de:
                // Titulos, natureza de lançamento e clientes
                var titulos = repositorioTitulos.Todos();

                var dados = new {
                      naturezas = repositorioNaturezaLancamento.Todos(),
                      clientes = repositorioUsuarios.Todos(),
                      titulos = repositorioTitulos.Todos()
                };

                return (Response) JsonConvert.SerializeObject(dados);
            });


        }
    }
}
