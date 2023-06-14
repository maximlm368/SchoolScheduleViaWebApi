using System;
using System.Threading.Tasks;
using ScheduleLogic;

namespace ScheduleAppLogic
{
    public class ClientsStorage
    {
        private static Dictionary<string , string>? loginToPassword = null;


        private protected ClientsStorage ( IClientsStorageDbGateway gateway )
        {
            if ( loginToPassword == null )
            {
                loginToPassword = gateway.GetLoginToPassword ( );
            }
        }


        private protected LoginPassCoincidence CheckClientAndAddIfNew ( string login , string password )
        {
            string pass = "";
            LoginPassCoincidence result = LoginPassCoincidence.loginAbsent;

            try
            {
                pass = loginToPassword [ login ];

                if ( pass == password )
                {
                    result = LoginPassCoincidence.bothCoincide;
                }
                else
                {
                    result = LoginPassCoincidence.passWrong;
                }
            }
            catch ( KeyNotFoundException )
            {
                result = LoginPassCoincidence.loginAbsent;
                loginToPassword.Add ( login , password );
            }

            return result;
        }


        private protected void RemoveClient ( string login )
        {
            loginToPassword.Remove ( login );
        }
    }



    public class LoginLogic : ClientsStorage, ILoginLogic
    {
        private IPreparerLoader preparersLoader;
        private static ILoginLogic singleItem = null;

        private LoginLogic ( IClientsStorageDbGateway clientsStorageDbGateway ,
                             IPreparersGetterAndSaverDbGateway preparersDbGateway ,
                             IAppInsideAbstractionImplementationProvider implementationProvider
                           ) : base ( clientsStorageDbGateway )
        {
            preparersLoader = ( IPreparerLoader )
                               implementationProvider.GetImplimentation ( typeof ( IPreparerLoader ) , new object [ 0 ] );
        }


        public static ILoginLogic GetLoginLogic ( IClientsStorageDbGateway clientsStorageDbGateway ,
                                                  IAppInsideAbstractionImplementationProvider implementationProvider ,
                                                  IPreparersGetterAndSaverDbGateway preparersDbGateway )
        {
            if ( singleItem == null )
            {
                singleItem = new LoginLogic ( clientsStorageDbGateway , preparersDbGateway , implementationProvider );
            }

            return singleItem;
        }


        public LoggingResultDto LogIn ( string login , string password )
        {
            LoggingState state = LoggingState.Failed;
            List<string> entities = null;

            var coincidence = base.CheckClientAndAddIfNew ( login , password );

            if ( coincidence == LoginPassCoincidence.bothCoincide )
            {
                entities = ScheduleCalculationDataPreparer.GetEntities ( );
                state = LoggingState.Logged;
                preparersLoader.LoadPreparerByLogin ( login );


                var attrs = typeof ( IPreparerLoader ).



            }
            else if ( coincidence == LoginPassCoincidence.loginAbsent )
            {
                base.RemoveClient ( login );
            }

            return new LoggingResultDto ( state , entities );
        }

    }



    public class RegisterLogic : ClientsStorage, IRegisterLogic
    {
        private IRegisterDBGateway dbGateway;
        private IActivePreparersStorage preparersStorage;
        private IPublicAbstractionImplementationProvider ImplimentationProvider;
        private static IRegisterLogic singleItem = null;


        private RegisterLogic ( IRegisterDBGateway regDbGateway ,
                                IPublicAbstractionImplementationProvider typeProvider ,
                                IAppInsideAbstractionImplementationProvider implementationProvider )
               : base ( ( IClientsStorageDbGateway ) regDbGateway )

        {
            dbGateway = regDbGateway;
            ImplimentationProvider = typeProvider;
            preparersStorage = ( IActivePreparersStorage ) implementationProvider.GetImplimentation ( typeof ( IActivePreparersStorage ) , null );
        }


        public static IRegisterLogic GetRegisterLogic ( IRegisterDBGateway regDbGateway ,
                                                        IPublicAbstractionImplementationProvider typeProvider ,
                                                        IAppInsideAbstractionImplementationProvider implementationProvider )
        {
            if ( singleItem == null )
            {
                singleItem = new RegisterLogic ( regDbGateway , typeProvider , implementationProvider );
            }

            return singleItem;
        }


        public Task<LoggingResultDto> Register ( string login , string password )
        {
            Task<bool>? registrationIsSuccess = null;
            Task<LoggingResultDto> resultDto = new Task<LoggingResultDto> ( ( ) => { return new LoggingResultDto ( LoggingState.Failed , null ); } );
            var coincidenceStatus = base.CheckClientAndAddIfNew ( login , password );

            if ( coincidenceStatus == LoginPassCoincidence.loginAbsent )
            {
                registrationIsSuccess = dbGateway.RegisterClient ( login , password );

                if ( registrationIsSuccess != null )
                {
                    resultDto = registrationIsSuccess.ContinueWith<LoggingResultDto> ( ( t , log ) => { return BuildResult ( t , log ); } , login );
                }
            }
            else
            {
                resultDto.RunSynchronously ( );
            }

            return resultDto;
        }


        private LoggingResultDto BuildResult ( Task<bool> registrationSuccess , object login )
        {
            string newLogin = ( string ) login;
            var entities = DomainEntities.GetEntities ( );
            var state = LoggingState.Logged;

            if ( registrationSuccess.Result == true )
            {
                preparersStorage.AddPreparerByLogin ( newLogin );
            }
            else
            {
                entities = null;
                state = LoggingState.Failed;
            }

            return new LoggingResultDto ( state , entities );
        }

    }

}