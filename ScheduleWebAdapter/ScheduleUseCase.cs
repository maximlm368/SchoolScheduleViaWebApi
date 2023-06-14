using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWebAdapter
{
    public class WebAdapter : ISchedluleUseCase
    {
        public WebAdapter ( )
        {
        }


        public event TriedToLoginEventHandler TriedToLoginEvent;
        public event GotEntitiesEventHandler GotEntitiesEvent;
        public event GotContentEventHandler GotContentEvent;


        public void LogIn ( string login , string password )
        {
            var httpClient = new HttpClient ( );
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


        public void Insert ( )
        {
            throw new NotImplementedException ( );
        }


        public void Update ( )
        {
            throw new NotImplementedException ( );
        }


        public void Delete ( )
        {
            throw new NotImplementedException ( );
        }


        public void GetEntities ( )
        {
            var httpClient = new HttpClient ( );
            var task = httpClient.GetAsync ( "" );
            task.ContinueWith
            (
                ( t ) =>
                {
                    HandleResult ( t );
                }
            );

        }


        private void HandleResult ( Task<HttpResponseMessage> task )
        {
            var str = task.Result.Content.ReadAsStringAsync ( );
            str.ContinueWith ( tsk => GotEntitiesEvent ( new List<string> { str.Result } ) );
        }


        public void LoadEntityContent ( string entityName )
        {
            var httpClient = new HttpClient ( );
            var task = httpClient.GetAsync ( "" );
            task.ContinueWith
            (
                ( t ) =>
                {
                    var str = task.Result.Content.ReadAsStringAsync ( );
                    str.ContinueWith ( tsk => GotContentEvent ( DeserializeToContent ( str.Result ) ) );
                }
            );
        }


        private EntityContent DeserializeToContent ( string contentStringPresentation )
        {
            return new EntityContent ( );
        }
    }

}