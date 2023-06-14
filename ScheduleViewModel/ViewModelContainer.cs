using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ScheduleWebAdapter;
//using ScheduleWebAPI.Controllers;

namespace ScheduleViewModel
{
    class ViewModelContainer : IViewModelContainer
    {
        private ISchedluleUseCase _schedulingModel;

        public IEntitiesViewModel EntitiesViewModel
        {
            get { return EntitiesViewModel; }
            private set { EntitiesViewModel = value; }
        }

        public ILoginViewModel LogInViewModel
        {
            get { return LogInViewModel; }
            private set { LogInViewModel = value; }
        }

        public IDataEditionViewModel DataEditionViewModel
        {
            get { return DataEditionViewModel; }
            private set { DataEditionViewModel = value; }
        }


        public ViewModelContainer ( ISchedluleUseCase scheduling , ILoginViewModel logViewModel , IEntitiesViewModel entViewModel , IDataEditionViewModel dataViewModel )
        {
            _schedulingModel = scheduling;
            this.DataEditionViewModel = dataViewModel;
            this.EntitiesViewModel = entViewModel;
            this.LogInViewModel = logViewModel;
        }


        public List<string> GetNames ( string entityName )
        {
            throw new NotImplementedException ( );
        }
    }
}