using System;
using System.Net.Http;
using System.Text;
//using ScheduleWebAPI.Controllers;

namespace ScheduleWebAdapter
{
    public class DataEditionUseCase : IDataEditionModel
    {
        public event GotContentEventHandler GotContentEvent;


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

