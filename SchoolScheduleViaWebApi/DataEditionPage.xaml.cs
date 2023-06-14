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
//using ScheduleUseCases;
using ScheduleViewModel;


namespace ScheduleView
{
    /// <summary>
    /// Логика взаимодействия для DataInsertGrid.xaml
    /// </summary>
    public partial class DataEditionPage : UserControl
    {
        private IViewModelContainer _viewModelContainer;
        private IDataEditionViewModel _viewModel;
        private Window _owner;
        private List<string> _columnNames;
        private List<List<string>> _entityContent;


        public DataEditionPage ( Window owner , IViewModelContainer viewModel , string entityName )
        {
            InitializeComponent ( );
            _viewModelContainer = viewModel;
            _viewModel = viewModel.DataEditionViewModel;
            _owner = owner;
            _columnNames = new List<string> ( );
            _viewModel.ColumnNamesAreReady += ShowColumnNames;
            _viewModel.ContentAreReady += ShowContent;
            _viewModel.LoadContent ( entityName );
        }


        private void ShowColumnNames ( object sender , ColumnNamesReadyEventArgs e )
        {
            _columnNames = e._names;
            var scrlViewer = ( ScrollViewer ) this.Content;
            var grid = ( Grid ) scrlViewer.Content;
            var rowDefs = grid.RowDefinitions;
            var labelRowNumber = 0;
            var textBoxRowNumber = 0;

            for ( int i = 0; i < rowDefs.Count; i++ )
            {
                if ( rowDefs [ i ].Name == labelRow.Name )
                {
                    labelRowNumber = i;
                }
                else if ( rowDefs [ i ].Name == textBoxRow.Name )
                {
                    textBoxRowNumber = i;
                }

                if ( labelRowNumber != 0 && textBoxRowNumber != 0 )
                {
                    break;
                }
            }

            SetUpInsertingRow ( grid , labelRowNumber , textBoxRowNumber );
        }


        private void SetUpInsertingRow ( Grid containingGrid , int labelRowNumber , int textBoxRowNumber )
        {
            for ( int j = 0; j < _columnNames.Count; j++ )
            {
                containingGrid.ColumnDefinitions.Add ( new ColumnDefinition ( ) );
                var columnOrdinalNumber = j + 1;

                var label = new Label ( );
                label.Content = _columnNames [ j ];
                Grid.SetRow ( label , labelRowNumber );
                Grid.SetColumn ( label , columnOrdinalNumber );
                containingGrid.Children.Add ( label );

                var textBox = new TextBox ( );
                Grid.SetRow ( textBox , textBoxRowNumber );
                Grid.SetColumn ( textBox , columnOrdinalNumber );
                containingGrid.Children.Add ( textBox );
            }
        }


        private void ShowContent ( object sender , ContentReadyEventArgs e )
        {
            _entityContent = e._content;
            var scrlViewer = ( ScrollViewer ) this.Content;
            var grid = ( Grid ) scrlViewer.Content;

            for ( int i = 0; i < _entityContent.Count; i++ )
            {
                grid.RowDefinitions.Add ( new RowDefinition ( ) );
                var rowOrdinalNumber = i + 4;

                for ( int j = 0; j < _entityContent [ i ].Count; j++ )
                {
                    var columnOrdinalNumber = j + 1;
                    var entityValue = new Label ( );
                    entityValue.Content = _columnNames [ j ];
                    Grid.SetRow ( entityValue , rowOrdinalNumber );
                    Grid.SetColumn ( entityValue , columnOrdinalNumber );
                    grid.Children.Add ( entityValue );
                }
            }

        }
    }
}
