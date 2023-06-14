using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppLogic;
using System;
using System.Threading.Tasks;

namespace ScheduleWebAPI
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class ScheduleLoginController : ControllerBase
    {
        private ILoginLogic loginLogic;


        public ScheduleLoginController ( ILoginLogic loginLogic )
        {
            this.loginLogic = loginLogic;
        }


        [HttpPost]
        public void LogIn ( string login , string password )
        {
            var loggingResult = loginLogic.LogIn ( login , password );
            HttpContext.Response.WriteAsJsonAsync ( loggingResult );
        }


        //private void WriteJsonResultIntoResponse ( LoggingState logState , List<string> entities )
        //{
        //    HttpContext.Response.WriteAsJsonAsync ( logState );
        //}



    }

}