using ScheduleLogic;

namespace ScheduleAppLogic
{
    internal interface IActivePreparersStorage
    {
        internal void AddPreparerByLogin ( string login );
    }


    internal interface IPreparerLoader
    {
        internal void LoadPreparerByLogin ( string login );
    }


    internal interface IPreparerUnloader
    {
        internal void UnloadPreparerByLogin ( string login );
    }
}