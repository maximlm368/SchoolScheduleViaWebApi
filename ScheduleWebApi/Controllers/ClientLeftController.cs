using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppLogic;

namespace ScheduleWebAPI
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class ScheduleClientLeftController : ControllerBase
    {
        private IClientLoggedOffLogic clientLogOffLogic;


        public ScheduleClientLeftController ( IClientLoggedOffLogic logOffLogic )
        {
            clientLogOffLogic = logOffLogic;
        }


        [HttpPost]
        public void LogOff ( string login )
        {
            Task task = new Task ( ( ) => { clientLogOffLogic.LogOff ( login ); } );
            task.Start ( );
        }
    }
}