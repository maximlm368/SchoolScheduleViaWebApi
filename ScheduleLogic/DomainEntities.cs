using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScheduleLogic
{
    public static class DomainEntities
    {
        public static List<string> GetEntities ( )
        {
            //return entityNames.clone ( );
            return entityNames;
        }

        public static readonly List<string> entityNames = new List<string> { "teachers" , "disciplines" , "groups" };

    }



    public interface IEntity
    {

    }



    public class Teacher : IEntity
    {
        private string _firstName;
        private string _secondName;
        private List<Discipline> _attachedDisciplines;
    }



    public class Group : IEntity
    {

    }



    public class Discipline : IEntity
    {

    }


}