using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWebAdapter
{
    public class LoginWebAdapter : ILoginModel
    {
        public void LogIn ( string login , string password )
        {

            var httpHandler = new HttpClientHandler ( );
            httpHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
            var httpClient = new HttpClient ( httpHandler );
            var task = httpClient.GetAsync ( "" );
            task.ContinueWith
            (
                ( t ) =>
                {
                    var str = task.Result.Content.ReadAsStringAsync ( );
                    str.ContinueWith ( tsk => TriedToLoginEvent ( DeserealizeToLoggingState ( str.Result ) ) );
                }
            );
        }


        private LoggingState DeserealizeToLoggingState ( string state )
        {
            return LoggingState.Logged;
        }


        public event TriedToLoginEventHandler TriedToLoginEvent;
    }
}