using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ScheduleViewModel
{
    public interface IEntitiesViewModel : INotifyPropertyChanged
    {
        void GetEntities ( );
    }
}