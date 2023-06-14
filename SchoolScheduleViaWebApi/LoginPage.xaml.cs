using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScheduleViewModel;
using System.ComponentModel;

namespace ScheduleView
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        private IViewModelContainer _viewModelContainer;
        private ILoginViewModel _viewModel;
        private Window _owner;


        public LoginPage ( Window owner , IViewModelContainer viewModel )
        {
            InitializeComponent ( );
            _viewModelContainer = viewModel;
            _viewModel = viewModel.LogInViewModel;
            _viewModel.PropertyChanged += ViewModelPropertyChanged;
            _owner = owner;

            var scrlViewer = ( ScrollViewer ) this.Content;
            var grid = ( Grid ) scrlViewer.Content;
            var fntSize = this.Width / 40;

            login.FontSize = fntSize;
            password.FontSize = fntSize;
            separator1.Height = new GridLength ( fntSize * 2 );
            separator2.Height = new GridLength ( fntSize * 2 );
            separator3.Height = new GridLength ( fntSize * 2 );
        }


        private void ViewModelPropertyChanged ( object sender , PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == "_loggingState" )
            {
                var logStateEvArgs = ( LoggingStateChangedEventArgs ) e;
                var value = logStateEvArgs._propertyValue;

                if ( value == LoggingState.Logged )
                {
                    ShowWindowWithEntities ( );
                    return;
                }

                if ( value == LoggingState.Failed )
                {
                    ShowLoginErrorMessage ( );
                }

            }
        }


        private void ShowWindowWithEntities ( )
        {
            _owner.Content = new EntitiesPage ( _owner , _viewModelContainer );
        }


        private void ShowLoginErrorMessage ( )
        {

        }


        private void LogIn ( object sender , RoutedEventArgs e )
        {
            string log = login.Text;
            string pass = password.Text;
            _viewModel.LogIn ( log , pass );
        }


        private void StepToRegister ( object sender , RoutedEventArgs e )
        {
            var button = ( Button ) sender;
            _owner.Content = new RegisterPage ( _owner , _viewModelContainer );
        }

    }
}
