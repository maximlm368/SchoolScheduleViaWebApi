using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ScheduleWebAdapter;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;

namespace ScheduleViewModel
{
    class EntitiesViewModel : IEntitiesViewModel
    {
        private IEntitiesModel _entitiesModel;

        //private Task _getEntitiesTask;

        private List<string> _entities
        {
            get { return _entities; }
            set
            {
                _entities = value;
                List<string> entities = _entities.clone ( );
                Task taskForGraphicThread = new Task
                (
                   ( ) => PropertyChanged ( this , new EntitiesPropertyChangedEventArgs ( "_entities" , entities ) )
                );
                taskForGraphicThread.Start ( TaskScheduler.FromCurrentSynchronizationContext ( ) );
            }
        }


        public EntitiesViewModel ( IEntitiesModel entitiesModel )
        {
            _entitiesModel = entitiesModel;
            _entitiesModel.GotEntitiesEvent += SendEntitiesToView;
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public void GetEntities ( )
        {
            Task getEntitiesTask = new Task ( ( ) => _entitiesModel.GetEntities ( ) );
            getEntitiesTask.Start ( );
        }


        private void SendEntitiesToView ( List<string> entityNames )
        {
            _entities = entityNames;
        }

    }

}