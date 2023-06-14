using ScheduleAppLogic;
using ScheduleLogic;
using System.Data.SqlClient;

namespace DBGateWay
{
    public class ClientsStorageDbGateway : IClientsStorageDbGateway
    {
        private static string _clientIdentificationCommandText;

        private static string DBName;


        static ClientsStorageDbGateway ( )
        {
            _clientIdentificationCommandText = "";
        }


        public Dictionary<string , string> GetLoginToPassword ( )
        {
            return LoginToPasswordGetter.GetLoginToPassword ( );
        }
    }



    static class LoginToPasswordGetter
    {
        internal static Dictionary<string , string> GetLoginToPassword ( )
        {
            var loginToPassword = new Dictionary<string , string> ( );

            try
            {
                string connenctionString = DbCreator.DBConnectionString;
                SqlConnection connection = new SqlConnection ( connenctionString );
                connection.Open ( );
                SqlCommand command = new SqlCommand ( );
                command.CommandText = "SELECT login , password FROM " + DbCreator.clientsContainingTableName + ";";
                command.Connection = connection;
                var dictionaryDataReader = command.ExecuteReaderAsync ( );

                while ( dictionaryDataReader.Read ( ) )
                {
                    var keyValue = new List<string> ( );

                    for ( var i = 0; i < dictionaryDataReader.FieldCount; i++ )
                    {
                        var str = dictionaryDataReader [ i ].ToString ( );
                        keyValue.Add ( str );
                    }

                    loginToPassword.Add ( keyValue [ 0 ] , keyValue [ 1 ] );
                }

                dictionaryDataReader.Close ( );
                connection.Close ( );
            }
            catch ( System.Data.SqlClient.SqlException )
            {
            }
            catch ( System.InvalidOperationException )
            {
            }

            return loginToPassword;
        }
    }



    public class RegistrationDBGateway : IClientsStorageDbGateway, IRegisterDBGateway
    {
        private RegistrationDBGateway ( )
        {

        }


        public Dictionary<string , string> GetLoginToPassword ( )
        {
            return LoginToPasswordGetter.GetLoginToPassword ( );
        }


        public Task<bool>? RegisterClient ( string login , string password )
        {
            Task<bool>? isSuccess = null;
            string connenctionString = DbCreator.DBConnectionString;
            SqlConnection connection = new SqlConnection ( connenctionString );

            try
            {
                connection.Open ( );
                SqlCommand command = new SqlCommand ( );
                command.CommandText = "INSERT INTO " + DbCreator.clientsContainingTableName
                                    + " (login, password) VALUES (" + login + ", " + password + ");";
                command.Connection = connection;
                var affectedRows = command.ExecuteNonQueryAsync ( );

                string str = "";

                isSuccess = affectedRows.ContinueWith<bool> ( ( antecedent , str ) => {
                    connection.Close ( );
                    var result = false;
                    if ( antecedent.Result > 0 || ( string ) str == "" )
                    {
                        result = true;
                    }

                    return result;
                } , str );

            }
            catch ( System.Data.SqlClient.SqlException )
            {
            }
            catch ( System.InvalidOperationException )
            {
            }
            return isSuccess;
        }

    }



    public class SavingPreparersDbGateway : IPreparersGetterAndSaverDbGateway
    {
        private bool storedProcedureIsCreated = false;

        private string tableName;

        private string preparerSavingStoredProcedureCreation = " create or alter proc MML.pr_savePreparer " +
                                         "@login nvarchar(30) " +
                                         " SELECT login FROM @login;";

        private string preparerSavingStoredProcedureExecution = "";

        private string prerparerGettingStoredProcedureCreation = " create or alter proc MML.pr_getPreparer " +
                                         "@login nvarchar(30)" +
                                         " SELECT login FROM " + DbCreator.clientsContainingTableName + "; ";

        private string preparerGettingStoredProcedureExecution = "";


        public SavingPreparersDbGateway ( )
        {
            if ( storedProcedureIsCreated )
            {
                return;
            }

            string connenctionString = DbCreator.DBConnectionString;
            SqlConnection connection = new SqlConnection ( connenctionString );

            try
            {
                connection.Open ( );
                SqlCommand command = new SqlCommand ( );
                command.CommandText = preparerSavingStoredProcedureCreation;
                command.Connection = connection;
                var affectedRows = command.ExecuteNonQueryAsync ( );
            }
            catch ( System.Data.SqlClient.SqlException )
            {

            }
            catch ( System.InvalidOperationException )
            {
            }
        }


        public void SavePreparer ( IPreparerWorkDuringSessionResultSubmitter keeper )
        {
            var deleted = keeper.GetIndexesOfDeleted ( typeof ( Teacher ) );
            var inserted = ( List<Teacher> ) keeper.GetInserted ( typeof ( Teacher ) );
            var teacher = inserted [ 0 ];

            Task<bool>? isSuccess = null;
            string connenctionString = DbCreator.DBConnectionString;
            SqlConnection connection = new SqlConnection ( connenctionString );

            try
            {
                connection.Open ( );
                SqlCommand command = new SqlCommand ( );
                command.CommandText = preparerSavingStoredProcedureExecution;
                command.Connection = connection;
                var affectedRows = command.ExecuteNonQueryAsync ( );

                string str = "";

                isSuccess = affectedRows.ContinueWith<bool> ( ( antecedent , str ) => {
                    connection.Close ( );
                    var result = false;
                    if ( antecedent.Result > 0 || ( string ) str == "" )
                    {
                        result = true;
                    }

                    return result;
                } , str );

            }
            catch ( System.Data.SqlClient.SqlException )
            {
            }
            catch ( System.InvalidOperationException )
            {
            }
        }


        public Task<IPreparer> GetPreparer ( string login )
        {
            Task<IPreparer>? isSuccess = null;
            string connenctionString = DbCreator.DBConnectionString;
            SqlConnection connection = new SqlConnection ( connenctionString );

            try
            {
                connection.Open ( );
                SqlCommand command = new SqlCommand ( );
                command.CommandText = preparerGettingStoredProcedureExecution;
                command.Connection = connection;
                var affectedRows = command.ExecuteNonQueryAsync ( );

                string str = "";

                isSuccess = affectedRows.ContinueWith<IPreparer> ( ( antecedentTask , str ) => {
                    connection.Close ( );
                    var result = false;
                    if ( antecedentTask.Result > 0 || ( string ) str == "" )
                    {
                        result = true;
                    }

                    return result;
                } , str );

            }
            catch ( System.Data.SqlClient.SqlException )
            {
            }
            catch ( System.InvalidOperationException )
            {
            }

            return isSuccess;
        }


        //public Dictionary<string , PersonalDataPreparer> GetPreparers ( )
        //{
        //    throw new NotImplementedException ( );
        //}
    }

}