namespace ScheduleWebAdapter
{
    public interface ISchedluleUseCase
    {
        void LogIn ( string login , string password );

        void Insert ( );

        void Update ( );

        void Delete ( );

        void GetEntities ( );

        void LoadEntityContent ( string entityName );

        event TriedToLoginEventHandler TriedToLoginEvent;

        event GotEntitiesEventHandler GotEntitiesEvent;

        event GotContentEventHandler GotContentEvent;
    }
}