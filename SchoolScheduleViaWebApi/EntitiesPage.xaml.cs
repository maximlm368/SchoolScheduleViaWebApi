
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
using System.ComponentModel;
using ScheduleViewModel;

namespace ScheduleView
{
    /// <summary>
    /// Логика взаимодействия для Entities.xaml
    /// </summary>
    public partial class EntitiesPage : UserControl
    {
        private IViewModelContainer _viewModelContainer;
        private IEntitiesViewModel _viewModel;
        private Window _owner;
        private List<string> _entities;


        public EntitiesPage ( Window owner , IViewModelContainer viewModel )
        {
            InitializeComponent ( );
            _viewModelContainer = viewModel;
            _viewModel = viewModel.EntitiesViewModel;
            _owner = owner;
            _viewModel.GetEntities ( );
            _viewModel.PropertyChanged += ShowEntities;
        }


        private void ShowEntities ( object sender , PropertyChangedEventArgs e )
        {
            var args = ( EntitiesPropertyChangedEventArgs ) e;
            _entities = args._propertyValue;
            var grid = ( Grid ) this.GetTemplateChild ( "grid" );
            var regardingSeparatorStep = 0;

            for ( int i = 0; i < _entities.Count; i++ )
            {
                var rowForButton = new RowDefinition ( );
                rowForButton.Name = _entities [ i ];
                rowForButton.Height = GridLength.Auto;
                grid.RowDefinitions.Add ( rowForButton );

                var separator = new RowDefinition ( );
                separator.Name = "separator" + ( i + 2 ).ToString ( );
                separator.Height = new GridLength ( 2 * rowForButton.Height.Value );
                grid.RowDefinitions.Add ( separator );

                var button = new Button ( );
                button.Content = _entities [ i ];
                button.Click += ShowWindowWithEntityContent;
                Grid.SetColumn ( button , i + 3 + regardingSeparatorStep );
                regardingSeparatorStep = regardingSeparatorStep + 2;
                grid.Children.Add ( button );
            }

        }


        private void ShowWindowWithEntityContent ( object sender , RoutedEventArgs e )
        {
            var button = ( Button ) sender;
            var entityName = button.Content.ToString ( );
            _owner.Content = new DataEditionPage ( _owner , _viewModelContainer , entityName );
        }

    }
}