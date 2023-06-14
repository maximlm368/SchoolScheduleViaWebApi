using System.Security.Cryptography;

namespace ScheduleAppLogic
{
    public interface ILoginLogic
    {
        LoggingResultDto LogIn ( string login , string password );
    }


    public interface IRegisterLogic
    {
        Task<LoggingResultDto> Register ( string login , string password );
    }


    public interface IDataEditionLogic
    {
        void AddItem ( string login , string entityName , object newItem );

        void ChangeItem ( string login , string entityName , object oldItem , object newItem );

        void DeleteItem ( string login , string entityName );

        void GetAllItemsOfEntity ( string login , string entityName );
    }


    public interface IClientLoggedOffLogic
    {
        void LogOff ( string login );
    }


    public interface IPublicAbstractionImplementationProvider
    {
        object? GetImplimentation ( Type abstraction );
    }


    public interface IAppInsideAbstractionImplementationProvider
    {
        object? GetImplimentation ( Type abstraction , object [ ]? args );
    }
}