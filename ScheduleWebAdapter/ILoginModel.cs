namespace ScheduleWebAdapter
{
    public interface ILoginModel
    {
        void LogIn ( string login , string password );

        event TriedToLoginEventHandler TriedToLoginEvent;
    }
}
