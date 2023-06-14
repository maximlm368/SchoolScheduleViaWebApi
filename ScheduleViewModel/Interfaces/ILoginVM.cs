using System;
using System.Collections.Generic;
using System.Text;
using ScheduleWebAdapter;
using System.ComponentModel;

namespace ScheduleViewModel
{
    public interface ILoginViewModel : INotifyPropertyChanged
    {
        void LogIn ( string login , string password );
    }
}