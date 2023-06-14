using System;
using System.Collections;

namespace ScheduleLogic
{
    internal abstract class KeepingChangesEntityListWrapper
    {
        protected IList items;
        protected IList addeds;
        protected IList updateds;
        protected List<int> deleteds;


        internal void AddItem<T> ( T entityValue )
        {
            items.Add ( entityValue );
            addeds.Add ( entityValue );
        }

        internal List<int> GetIndexesOfDeleted ( )
        {
            return deleteds;
        }

        internal IList GetInserted ( )
        {
            return addeds;
        }

        internal IList GetUpdated ( )
        {
            return updateds;
        }
    }



    internal class TeachersKeeper : KeepingChangesEntityListWrapper
    {
        public TeachersKeeper ( )
        {
            items = new List<Teacher> ( );
            addeds = new List<Teacher> ( );
            updateds = new List<Teacher> ( );
            deleteds = new List<int> ( );

            items [ 0 ] = new Teacher ( );
        }

    }



    internal class DisciplinesKeeper : KeepingChangesEntityListWrapper
    {
    }

}