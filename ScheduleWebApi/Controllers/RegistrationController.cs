using Microsoft.AspNetCore.Mvc;
using ScheduleAppLogic;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScheduleWebAPI
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class ScheduleRegistrationController : ControllerBase
    {
        private IRegisterLogic registerLogic;


        public ScheduleRegistrationController ( IRegisterLogic regLogic )
        {
            registerLogic = regLogic;
        }


        //POST api/<ScheduleRegisterController>
        [HttpPost]
        public void Register ( string login , string password )
        {
            Task<LoggingResultDto> task = registerLogic.Register ( login , password );
            task.Start ( );
            task.ContinueWith ( ( t ) => { HttpContext.Response.WriteAsJsonAsync ( t.Result ); } );
        }


        //[HttpPost]
        //public async void Register ( string login , string password )
        //{
        //    LoggingResultDto resultDto = await registerLogic.Register ( login , password );
        //    WriteJsonResponse ( resultDto );
        //}


        //private void WriteJsonResponse ( LoggingResultDto result )
        //{
        //    HttpContext.Response.WriteAsJsonAsync ( result );
        //}
    }
}