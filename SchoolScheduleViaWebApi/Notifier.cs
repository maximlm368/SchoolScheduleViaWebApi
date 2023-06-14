using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ScheduleView
{
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged (
            string propertyName )
        {
            PropertyChanged ( this , new PropertyChangedEventArgs ( propertyName ) );
        }
    }
}