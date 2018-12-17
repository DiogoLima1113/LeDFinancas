using Nancy;
using Nancy.Security;
using Newtonsoft.Json;

namespace NancyComWebSocket.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            //this.RequiresAuthentication();
            Get("/", _ =>
            {
                return View["index.html"];
            });

            Get("/cadastro", _ =>
            {
                return View["cadastro.html"];
            });

            Get("/relatorio", _ =>
            {
                return View["relatorio.html"];
            });
            
            Get("/configuracoes", _ =>
            {
                return View["configuracoes.html"];
            });
        }
    }
}
