using System;
using Nancy;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Nancy.Authentication.Forms;
using NancyComWebSocket.Autenticacao;
using NancyComWebSocket.Dominio.Repositorios.Interfaces;
using NancyComWebSocket.Dominio.Servico;
using NancyComWebSocket.Dominio.Repositorio;
using NancyComWebSocket.Dominio.Repositorios.Classes;

namespace NancyComWebSocket
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private IConfiguration Configurator;
        private IHostingEnvironment Env;

        private IServiceProvider ServiceProvider;
        private ILogger Log;
        private IRepositorioUsuarios repositorioUsuario;
        private IRepositorioNaturezaLancamento repositorioNaturezaLancamento;
        private IRepositorioTitulos repositorioTitulos;
        private ServicoAutenticacao servicoAutenticacao;

        public Bootstrapper(IConfiguration configurator, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
           this.Configurator = configurator;
           this.Env = env;
           this.ServiceProvider = serviceProvider;
           this.Log = Serilog.Log.Logger;
        }

        // É sempre a primeira coisa a ser iniciada pelo seu sistema, semelhante ao form_load
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            Log.Information("Sistema sendo iniciado");

            container.Register<IConfiguration>(Configurator);
            container.Register<IHostingEnvironment>(Env);
                    
            RegistrarRepositoriosEServicos(container);

            pipelines.BeforeRequest += LogBefore;
            pipelines.AfterRequest += LogAfter;

            Log.Information("NancyComWebSocket iniciado com sucesso");
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            // São os container que vão cuidar da autentificação do usuário.
            base.ConfigureRequestContainer(container, context);
            container.Register<IUserMapper>(AutenticadorUsuario.GetInstance());
            container.Register<IAutenticador>(AutenticadorUsuario.GetInstance());
        }

        // Quando acontece a primeira requisição, bate aqui dentro.
        protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
        {
            // Verifica se uma requisição para logar no sistema.
            var disableRedirect = (context.Request.Path == "/login" && 
                                context.Request.Method == "POST");

            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = "/login", // Pra onde foi direcionar.
                    UserMapper = requestContainer.Resolve<IUserMapper>(),
                    DisableRedirect = disableRedirect
                };

            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        }

        // É executado sempre antes de um usuário fazer a requisição.
        private Response LogBefore(NancyContext ctx)
        {
            var loginUsuario = string.Empty;
            var user = ctx.CurrentUser;
            var json = string.Empty;
            
            // if(user != null && user.Identity != null && user.Identity.IsAuthenticated)
            //     loginUsuario = user.AsUsuario().Funcionario.Login;

            // var positionBefore = ctx.Request.Body.Position;
            // var streamCopy = new MemoryStream();
            // ctx.Request.Body.CopyTo(streamCopy);
            // byte[] byteInput = streamCopy.ToArray();
            // streamCopy.Seek(0, SeekOrigin.Begin);

            // ctx.Request.Body.Position = positionBefore;

            Log.Information(string.Format("[{0}] {1} - {2} - Request Recebido", ctx.Request.Method, ctx.Request.Path, loginUsuario));
            return null;
        }

        // É Executado sempre depois de uma requisição.
        private void LogAfter(NancyContext ctx)
        {
            var loginUsuario = string.Empty;
            var user = ctx.CurrentUser;
            
            if(user != null && user.Identity != null && user.Identity.IsAuthenticated)
                try
                {
                    loginUsuario = user.Identity.Name;
                }
                catch(Exception)
                {
                    // TODO: Alguns momentos dá erro quando o usuário faz logoff. Investigar o motivo
                }

            Log.Information($"[{ctx.Request.Method}] {ctx.Request.Path} {ctx.Response.StatusCode}");
        }

        // Metodo que utilizo sempre para registrar um novo repositorio ou serviço para utilizar através de injeção de dependencias dentrod e um modulo.
        private void RegistrarRepositoriosEServicos(TinyIoCContainer container)
        {
            container.Register<ILogger>(Serilog.Log.Logger);

             var cp = new ConnectionProvider(Configurator);
             container.Register<IConnectionProvider>(cp);

            repositorioUsuario = new DBRepositorioUsuarios(cp);
            servicoAutenticacao = new ServicoAutenticacao(repositorioUsuario);
            repositorioNaturezaLancamento = new DBRepositorioNaturezaLancamento(cp);
            repositorioTitulos = new DBRepositorioTitulos(cp);

            container.Register<IRepositorioUsuarios>(repositorioUsuario);
            container.Register<ServicoAutenticacao>(servicoAutenticacao);
            container.Register<IRepositorioNaturezaLancamento>(repositorioNaturezaLancamento);
            container.Register<IRepositorioTitulos>(repositorioTitulos);

            var autenticador = AutenticadorUsuario.GetInstance();
            container.Register<IAutenticador>(autenticador);
        }

        protected override IRootPathProvider RootPathProvider
        {
            get { return new CustomRootPathProvider(Env); }
        }

    }

    public class CustomRootPathProvider : IRootPathProvider
    {
        private IHostingEnvironment Env;
        public CustomRootPathProvider(IHostingEnvironment env)
        {
            Env = env;
        }
        public string GetRootPath()
        {
            return Env.WebRootPath;
        }
    }
}