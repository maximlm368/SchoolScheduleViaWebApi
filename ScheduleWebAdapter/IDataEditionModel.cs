using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleWebAdapter
{
    public interface IDataEditionModel
    {
        void Insert ( );

        void Update ( );

        void Delete ( );

        void LoadEntityContent ( string entityName );

        event GotContentEventHandler GotContentEvent;
    }
}