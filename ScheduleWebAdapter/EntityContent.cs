using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleWebAdapter
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



    public delegate void GotEntitiesEventHandler ( List<string> entitieNames );



    public delegate void TriedToLoginEventHandler ( LoggingState logState );



    public delegate void GotContentEventHandler ( EntityContent content );



    public enum LoggingState
    {
        Neutral,
        Logged,
        Failed
    }

}