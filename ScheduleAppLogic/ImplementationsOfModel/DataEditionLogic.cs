using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleLogic;

namespace ScheduleAppLogic
{
    public class DataEdition : IDataEditionLogic, IActivePreparersStorage, IPreparerLoader, IPreparerUnloader
    {
        private IPreparersGetterAndSaverDbGateway dbGateway;
        private IAppInsideAbstractionImplementationProvider implementationProvider;
        private static Dictionary<string , IPreparer> loginToPreparerDictionary;
        private static IDataEditionLogic singleItem = null;


        private DataEdition ( IPreparersGetterAndSaverDbGateway dbGateway , IAppInsideAbstractionImplementationProvider implementationProvider )
        {
            this.dbGateway = dbGateway;
            this.implementationProvider = implementationProvider;
            loginToPreparerDictionary = new Dictionary<string , IPreparer> ( );
        }


        public static IDataEditionLogic GetSingleItem ( IPreparersGetterAndSaverDbGateway dbGateway ,
                                                        IAppInsideAbstractionImplementationProvider implementationProvider )
        {
            if ( singleItem == null )
            {
                singleItem = new DataEdition ( dbGateway , implementationProvider );
            }

            return singleItem;
        }


        #region internal interfaces methods to add, load, unload preparer in dictionary

        void IActivePreparersStorage.AddPreparerByLogin ( string login )
        {
            IPreparer preparer = ( IPreparer ) implementationProvider.GetImplimentation ( typeof ( IPreparer ) , new object [ ] { login } );
            loginToPreparerDictionary.Add ( login , preparer );
        }


        void IPreparerLoader.LoadPreparerByLogin ( string login )
        {
            var preparer = dbGateway.GetPreparer ( login );
            loginToPreparerDictionary.Add ( login , preparer.Result );
        }


        void IPreparerUnloader.UnloadPreparerByLogin ( string login )
        {
            dbGateway.SavePreparer ( ( IPreparerWorkDuringSessionResultSubmitter ) loginToPreparerDictionary [ login ] );
        }

        #endregion


        public void AddItem ( string login , string entityName , object newItem )
        {
            var preparer = loginToPreparerDictionary [ login ];
        }


        public void ChangeItem ( string login , string entityName , object oldItem , object newItem )
        {
            var preparer = loginToPreparerDictionary [ login ];
        }


        public void DeleteItem ( string login , string entityName )
        {
            var preparer = loginToPreparerDictionary [ login ];
        }


        public void GetAllItemsOfEntity ( string login , string entityName )
        {
            var preparer = loginToPreparerDictionary [ login ];
        }

    }
}