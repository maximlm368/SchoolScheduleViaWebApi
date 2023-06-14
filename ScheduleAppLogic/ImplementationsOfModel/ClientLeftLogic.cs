using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ScheduleLogic;

namespace ScheduleAppLogic
{
    public class ClientLoggedOffLogic : IClientLoggedOffLogic
    {
        private IPreparerUnloader unloader;

        public ClientLoggedOffLogic ( IAppInsideAbstractionImplementationProvider implementationProvider )
        {
            unloader = ( IPreparerUnloader )
                               implementationProvider.GetImplimentation ( typeof ( IPreparerUnloader ) , new object [ 0 ] );
        }


        public void LogOff ( string login )
        {
            unloader.UnloadPreparerByLogin ( login );
        }
    }
}