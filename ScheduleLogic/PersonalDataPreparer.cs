using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleLogic
{
    //public interface IMyDictionary
    //{
    //    void Add<T> ( Type key , KeepingChangesEntityListWrapper<T> value );

    //    KeepingChangesEntityListWrapper<T> Get <T> ( Type key );
    //}



    //public class MyDictionary : IMyDictionary
    //{
    //    private List<Type> keys;
    //    private List<object> values;

    //    public MyDictionary ()
    //    {
    //        keys = new List<Type> ( );
    //        values = new List<object> ( );
    //    }


    //    public void Add <T> ( Type key , KeepingChangesEntityListWrapper<T> value )
    //    {
    //        if ( key.FullName == value.GetType ( ).GetGenericArguments ( ) [ 0 ].FullName )
    //        {
    //            keys.Add ( key );
    //            values.Add ( value );
    //        }
    //    }


    //    public KeepingChangesEntityListWrapper<T> Get <T> ( Type key )
    //    {
    //        int index = keys.IndexOf ( key );
    //        var value = ( KeepingChangesEntityListWrapper<T> ) values [ index ];
    //        return value;
    //    }
    //}

    public interface IPreparer
    {
        void AddDomainEntityItem<T> ( T entityValue );

        void UpdateDomainEntityItem ( int itemId , object newValue );

        void DeleteDomainEntityItem ( Type entityType , int itemId );

        void GetEntityContent ( string entityName );
    }



    public interface IPreparerWorkDuringSessionResultSubmitter
    {
        List<int> GetIndexesOfDeleted ( Type entityType );

        IList GetInserted ( Type entityType );

        IList GetUpdated ( Type entityType );

        //KeepingChangesEntityWrapper<T> GetEntity <T> ( );
    }



    public class ScheduleCalculationDataPreparer : IPreparer, IPreparerWorkDuringSessionResultSubmitter
    {
        private KeepingChangesEntityListWrapper teachers;

        private KeepingChangesEntityListWrapper disciplines;

        private List<Group> groups;

        private Dictionary<Type , KeepingChangesEntityListWrapper> entitiesDictionary;


        public ScheduleCalculationDataPreparer ( string owner )
        {
            teachers = new TeachersKeeper ( );
            disciplines = new DisciplinesKeeper ( );
            groups = new List<Group> ( );
            var teacherType = Type.GetType ( "ScheduleModel.Teacher" );
            var disciplineType = Type.GetType ( "ScheduleModel.Discipline" );

            entitiesDictionary = new Dictionary<Type , KeepingChangesEntityListWrapper> ( );

            entitiesDictionary.Add ( teacherType , teachers );
            entitiesDictionary.Add ( disciplineType , disciplines );

        }


        #region IPreparer

        public void AddDomainEntityItem<T> ( T entityValue )
        {
            var entityType = entityValue.GetType ( );
            var entityWrapper = entitiesDictionary [ entityType ];
            entityWrapper.AddItem ( entityValue );
        }


        public void UpdateDomainEntityItem ( int itemId , object newValue )
        {
            throw new NotImplementedException ( );
        }


        public void DeleteDomainEntityItem ( Type entityType , int itemId )
        {
            throw new NotImplementedException ( );
        }


        public void GetEntityContent ( string entityName )
        {
            throw new NotImplementedException ( );
        }
        #endregion


        #region IPreparerWorkDuringSessionResultSubmitter

        public List<int> GetIndexesOfDeleted ( Type entityType )
        {
            var entity = entitiesDictionary [ entityType ];
            var deleted = entity.GetIndexesOfDeleted ( );
            return deleted;
        }


        public IList GetInserted ( Type entityType )
        {
            var entity = entitiesDictionary [ entityType ];
            var inserteds = entity.GetInserted ( );
            return inserteds;
        }


        public IList GetUpdated ( Type entityType )
        {
            var entity = entitiesDictionary [ entityType ];
            var updateds = entity.GetUpdated ( );
            return updateds;
        }
        #endregion
    }

}