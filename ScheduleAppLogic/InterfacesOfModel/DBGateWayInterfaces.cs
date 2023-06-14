using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleLogic;

namespace ScheduleAppLogic
{
    public interface IClientsStorageDbGateway
    {
        Dictionary<string , string> GetLoginToPassword ( );
    }


    public interface IRegisterDBGateway
    {
        Task<bool>? RegisterClient ( string login , string password );
    }


    public interface IPreparersGetterAndSaverDbGateway
    {
        Task<IPreparer> GetPreparer ( string login );

        void SavePreparer ( IPreparerWorkDuringSessionResultSubmitter keeper );
    }


    //public interface IPreparersSaverDbGateway
    //{
    //    void SavePreparer ( IPreparerWorkDuringSessionResultSubmitter keeper );
    //}


    //public interface IPersonalDataEditionDBGateway
    //{
    //    EntityContent GetContent ( string login , string entityName );

    //    void AddEntityItem ( object item );

    //    List<string> SelectEntities ( string login );

    //    void SetOwner ( string login );
    //}


    public enum LoginPassCoincidence
    {
        bothCoincide,
        passWrong,
        loginAbsent
    }

}