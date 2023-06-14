using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ScheduleWebAdapter;

namespace ScheduleViewModel
{
    public interface IViewModelContainer
    {
        IEntitiesViewModel EntitiesViewModel { get; }

        ILoginViewModel LogInViewModel { get; }

        IDataEditionViewModel DataEditionViewModel { get; }


        List<string> GetNames ( string entityName );
    }
}