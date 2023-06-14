using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ScheduleViewModel
{
    public interface IDataEditionViewModel : INotifyPropertyChanged
    {
        void Insert ( );

        void Update ( );

        void Delete ( );

        void LoadContent ( string entityName );

        event ColumnsReadyEventHandler ColumnNamesAreReady;

        event ContentReadyEventHandler ContentAreReady;
    }



    public delegate void ColumnsReadyEventHandler ( object sender , ColumnNamesReadyEventArgs e );



    public delegate void ContentReadyEventHandler ( object sender , ContentReadyEventArgs e );



    public class ColumnNamesReadyEventArgs
    {
        public List<string> _names;

        public ColumnNamesReadyEventArgs ( List<string> names )
        {
            _names = names;
        }
    }



    public class ContentReadyEventArgs
    {
        public List<List<string>> _content;

        public ContentReadyEventArgs ( List<List<string>> content )
        {
            _content = content;
        }
    }
}