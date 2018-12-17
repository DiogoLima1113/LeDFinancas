using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace NancyComWebSocket.Autenticacao
{
    public class RoboAutenticacao
    {
        private Timer timer;
        private IAutenticador autenticador;

        public RoboAutenticacao()
        {
            this.autenticador = AutenticadorUsuario.GetInstance();
            initTimer();
        }

        public RoboAutenticacao(IAutenticador aut)
        {
            this.autenticador = aut;
            initTimer();
        }

        private void initTimer()
        {
            timer = new Timer();
            timer.Interval = new TimeSpan(2, 0, 0).TotalMilliseconds;
            timer.Elapsed += (s, e) =>
            {
                autenticador.RemoverSessoesExpiradas();
            };
        }

        public void IniciarRoboAutenticacao()
        {
            timer.Start();
        }

    }
}
