using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleWebAdapter
{
    public interface IEntitiesModel
    {
        void GetEntities ( );

        event GotEntitiesEventHandler GotEntitiesEvent;
    }
}