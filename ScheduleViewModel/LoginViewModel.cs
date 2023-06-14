using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScheduleWebAdapter;
using System.ComponentModel;

namespace ScheduleViewModel
{
    class LoginViewModel : ILoginViewModel
    {
        private ILoginModel _loginModel;

        private LoggingState _loggingState
        {
            get { return _loggingState; }
            set
            {
                _loggingState = value;
                Task taskForGraphicThread = new Task
                (
                    ( ) => PropertyChanged ( this , new LoggingStateChangedEventArgs ( "_loggingState" , _loggingState , "" ) )
                );
                taskForGraphicThread.Start ( TaskScheduler.FromCurrentSynchronizationContext ( ) );
            }
        }


        public LoginViewModel ( ILoginModel logModel )
        {
            _loginModel = logModel;
            _loggingState = LoggingState.Neutral;
            _loginModel.TriedToLoginEvent += SendLoggingResToView;
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public void LogIn ( string login , string password )
        {
            Task loginTask = Task.Run ( ( ) => _loginModel.LogIn ( login , password ) );
        }


        private void SendLoggingResToView ( LoggingState logState )
        {
            _loggingState = logState;
        }

    }

}