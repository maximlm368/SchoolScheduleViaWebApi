using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleAppLogic;
using ScheduleAppLogic.ModelInterfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScheduleWebAPI
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class ScheduleDataEditionController : ControllerBase
    {
        private IDataEditionLogic editionLogic;


        public ScheduleDataEditionController ( IServiceProvider provider , IDataEditionLogic editLogic , string login )
        {
            var Provider = provider;
            var InjectedService = Provider.GetService<IPersonalDataEditionDBGateway> ( );

            editionLogic = editLogic;
            editionLogic.GotContentEvent += WriteJsonResultIntoResponse;
        }


        private void WriteJsonResultIntoResponse ( EntityContent content )
        {
            HttpContext.Response.WriteAsJsonAsync ( content );
        }


        [HttpGet]
        public void GetEntityContent ( string login , string entityName )
        {
            Task task = new Task ( ( ) => { editionLogic.GetAllItemsOfEntity ( login , entityName ); } );
            task.Start ( );
        }


        [HttpPost]
        public void AddItem ( )
        {
            string? entityTypeName = Request.Form [ "entityName" ];
            object? entityValue = null;
            Type? entityType = Type.GetType ( entityTypeName );

            if ( entityTypeName != null && entityType != null )
            {
                entityValue = JsonSerializer.Deserialize ( Request.Form [ "entityValue" ] , entityType );
            }

            if ( entityTypeName != null && entityValue != null )
            {
                Task task = new Task ( ( ) => { editionLogic.Insert ( entityTypeName , entityValue ); } );
                task.Start ( );
            }


        }
    }
}