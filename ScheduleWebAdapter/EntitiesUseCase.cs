using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWebAdapter
{
    public class EntitiesUseCase : IEntitiesModel
    {
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


        public event GotEntitiesEventHandler GotEntitiesEvent;
    }
}