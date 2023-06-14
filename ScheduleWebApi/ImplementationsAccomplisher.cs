using ScheduleAppLogic;
using DBGateWay;

namespace ScheduleWebAPI
{
    public class PreparersStorageFactory : IServiceProvider
    {
        public object? GetService ( Type serviceType )
        {
            switch ( serviceType.FullName )
            {
                //case "DBGateWay.PreparersSharedStorage":
                //   return new PreparersSharedStorage ( new SavingPreparersDbGateway ( ) );
                //break;

                case "ScheduleModel.LoginDBGateway":
                return new ClientsStorageDbGateway ( );
                break;

                default:
                break;
            }


            return null;
        }
    }



    public class ImplementationProvider : IPublicAbstractionImplementationProvider
    {
        private IServiceCollection services;


        public ImplementationProvider ( IServiceCollection services )
        {
            this.services = services;
        }


        public object? GetImplimentation ( Type abstraction )
        {
            var serviceProvider = services.BuildServiceProvider ( );
            var gateway = serviceProvider.GetService ( abstraction );

            return gateway;
        }
    }
}