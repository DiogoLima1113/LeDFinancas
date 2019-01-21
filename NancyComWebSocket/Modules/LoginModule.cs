
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using System;
using System.Configuration;
using System.Dynamic;
using NancyComWebSocket.Autenticacao;
using NancyComWebSocket.Dominio.Repositorio;
using NancyComWebSocket.Dominio.Entidades;
using NancyComWebSocket.Dominio.Repositorios.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

using Serilog;
using NancyComWebSocket.Dominio.Servico;

namespace NancyComWebSocket.NancyModules
{
    public class LoginModule : NancyModule
    {
        private ILogger Log;
        
        public LoginModule(IConfiguration configuration,
                           IHostingEnvironment env,
                           IAutenticador autenticador,
                           ILogger log,
                           ServicoAutenticacao servicoAutenticacao,
                           IRepositorioUsuarios repUsuarios)
        {
            Log = log;

            // Saber se o usuário esta logado e direcionar para a tela d elogin caso não esteja.
            Get("/login", _ =>
            {
                if (this.Context.CurrentUser != null)
                    return Response.AsRedirect("/");

                dynamic model = new ExpandoObject();
                model.Errored = this.Request.Query.error.HasValue;
                return View["login", model];
            });

            // Autenticar o usuário, dar acessos.
            Post("/login", _ =>
            {
                Log.Information("Tentativa de login sendo realizada - usuario -> " + (string) Request.Form.Usuario);

                var now = DateTime.Now;
                var expires = now.AddDays(1)
                    .AddHours(-now.Hour)
                    .AddMinutes(-now.Minute)
                    .AddSeconds(-now.Second)
                    .AddMilliseconds(-now.Millisecond)
                    .AddSeconds(-1);

                var login = ((string)this.Request.Form.Usuario).ToLower();
                var senha = (string)this.Request.Form.Senha;
                var mensagemErro = string.Empty;
              
        
              
                var usuarioAutenticado = servicoAutenticacao.ObterUsuarioAutenticado(login, senha);
                 
                if(usuarioAutenticado.Id < 1)
                {
                    return this.Context.GetRedirect("~/login?" + (string) Request.Form.Usuario + "#erroLogin");
                }

                var guid = autenticador.InserirSessao(usuarioAutenticado, expires);
                  
                
                Log.Information("Login realizado com sucesso - usuario -> " + Request.Form.Usuario);
                return this.LoginAndRedirect(guid, expires);
            });

            Get("/logout", _ =>
            {
                try
                {
                    // Fazer a rotina de logaut do sistema, obter o usuário logado para remover a sessão do mesmo.
                    // Fazer algo semelhante abaixo.

                     if (this.Context.CurrentUser != null)
                     {   
                        var usuario = repUsuarios.Obter(this.Context.CurrentUser.Identity.Name);
                        autenticador.Remover(autenticador.Obter(usuario.Guid));
                     }
                         
                }
                catch (AutenticacaoException)
                {
                    // Se deu exception, o usuario nao esta mais logado.
                }

                return this.LogoutAndRedirect("~/login");
            });
        }
    }
}
