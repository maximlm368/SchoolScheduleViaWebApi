using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Text;
using ScheduleWebAdapter;

namespace ScheduleViewModel
{
    class DataEditionViewModel : IDataEditionViewModel
    {
        private IDataEditionModel _dataEditionModel;
        private List<string> _columnNames;
        private List<List<string>> _entityContent;


        public DataEditionViewModel ( IDataEditionModel dataEditionModel )
        {
            _dataEditionModel = dataEditionModel;
            _dataEditionModel.GotContentEvent += SendContentToView;
        }


        private void SendContentToView ( EntityContent content )
        {
            var columnNames = content._columnNames;
            var contentBody = content._contentBody;

            var columnsReadyArgs = new ColumnNamesReadyEventArgs ( columnNames );
            var contentReadyArgs = new ContentReadyEventArgs ( contentBody );

            ColumnNamesAreReady ( this , columnsReadyArgs );
            ContentAreReady ( this , contentReadyArgs );
        }


        public void LoadContent ( string entityName )
        {
            _dataEditionModel.LoadEntityContent ( entityName );
        }


        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        public event ColumnsReadyEventHandler ColumnNamesAreReady;

        public event ContentReadyEventHandler ContentAreReady;
        #endregion

        #region EditionMethods

        public void Delete ( )
        {
            throw new NotImplementedException ( );
        }


        public void Insert ( )
        {
            throw new NotImplementedException ( );
        }



        public void Update ( )
        {
            throw new NotImplementedException ( );
        }
        #endregion

    }
}