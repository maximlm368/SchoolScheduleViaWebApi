using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScheduleWebAdapter;
using System.ComponentModel;

namespace ScheduleViewModel
{
    public class EntitiesPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        public List<string> _propertyValue { get; private set; }


        public EntitiesPropertyChangedEventArgs ( string propertyName , List<string> propertyValue ) : base ( propertyName )
        {
            _propertyValue = propertyValue;
        }
    }



    public class LoggingStateChangedEventArgs : PropertyChangedEventArgs
    {
        public LoggingState _propertyValue { get; private set; }
        public string _message { get; private set; }

        public LoggingStateChangedEventArgs ( string propertyName , LoggingState propertyValue , string message ) : base ( propertyName )
        {
            _propertyValue = propertyValue;
            _message = message;
        }
    }

}