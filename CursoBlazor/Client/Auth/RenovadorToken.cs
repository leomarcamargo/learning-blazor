using System;
using System.Timers;

namespace CursoBlazor.Client.Auth
{
    public class RenovadorToken : IDisposable
    {
        private readonly ILoginService _loginService;
        private Timer _timer;

        public RenovadorToken(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public void Iniciar()
        {
            _timer = new Timer
            {
                Interval = 1000 * 60 * 4
            };
            //4 minutos
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _loginService.ConduzirRenovacaoToken();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
