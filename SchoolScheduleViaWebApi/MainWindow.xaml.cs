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


namespace ScheduleView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IViewModelContainer _viewModel;


        public MainWindow ( IViewModelContainer viewModel )
        {
            InitializeComponent ( );

            this.Content = new LoginPage ( this , viewModel );
            _viewModel = viewModel;
        }
    }
}
