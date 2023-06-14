using System;
using System.Collections.Generic;
using System.Text;


namespace ScheduleAppLogic
{
    public class EntityContent
    {
        public List<string> _columnNames { get; private set; }

        public List<List<string>> _contentBody { get; private set; }


        public EntityContent ( List<string> columnNames , List<List<string>> contentBody )
        {
            _columnNames = columnNames;
            _contentBody = contentBody;
        }
    }



    //public delegate void GotEntitiesEventHandler ( List<string> entityNames );

    //public delegate void TriedToLoginEventHandler ( LoggingResultDto  loggingResult );

    //public delegate void GotContentEventHandler ( EntityContent content );



    public enum LoggingState
    {
        Neutral,
        Logged,
        Failed
    }



    public class LoggingResultDto
    {
        public LoggingState loggingState;

        public List<string>? entities;


        public LoggingResultDto ( LoggingState loggingState , List<string>? entities )
        {
            this.loggingState = loggingState;
            this.entities = entities;
        }
    }



    public class AppInsideImplementationProvider : IAppInsideAbstractionImplementationProvider
    {
        public object? GetImplimentation ( Type abstraction , object [ ]? args )
        {
            throw new NotImplementedException ( );
        }
    }
}