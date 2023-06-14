using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ScheduleAppLogic;
using System.Reflection.PortableExecutable;

namespace DBGateWay
{
    internal static class DbCreator
    {
        private static bool clientsDBIsSet = false;

        private static readonly string clientsDBCreationCommand = "CREATE DATABASE DB_SCHEDULE_CLIENTS";

        private static readonly string clientsTableCreationCommand = "CREATE TABLE " + clientsContainingTableName +
                                                                     "( client_id SMALLINT PRIMARY KEY, " +
                                                                     "login NVARCHAR(30) NOT NULL, " +
                                                                     "password NVARCHAR(30) NOT NULL )";

        //private static readonly string preparersTableCreationCommand = "CREATE TABLE " + preparersContainingTableName +
        //                                                             "( client_id SMALLINT PRIMARY KEY, " +
        //                                                             "login NVARCHAR(30) NOT NULL, " +
        //                                                             "password NVARCHAR(30) NOT NULL )";

        private static string DBMSConnectionString = @"Data Source=localhost\sqlexpress01;  Integrated Security=True";


        internal static readonly string DBConnectionString = @"Data Source = localhost\sqlexpress01; 
                                                      Initial   Catalog=DB_SCHEDULE_CLIENTS;   Integrated Security=True";

        //private static Dictionary<string , string> loginToPassword;

        internal static readonly string clientsContainingTableName = "MML.tb_clients";

        //internal static readonly string preparersContainingTableName = "MML.tb_preparers";


        static DbCreator ( )
        {
            if ( clientsDBIsSet )
            {
                return;
            }

            try
            {
                string connenctionString = DBMSConnectionString;
                SqlConnection connection = new SqlConnection ( connenctionString );
                connection.Open ( );
                SqlCommand command = new SqlCommand ( );
                command.CommandText = clientsDBCreationCommand;
                command.Connection = connection;
                command.ExecuteNonQuery ( );

                command.CommandText = clientsTableCreationCommand;
                command.Connection = connection;
                command.ExecuteNonQuery ( );
                connection.Close ( );

                clientsDBIsSet = true;
            }
            catch ( System.Data.SqlClient.SqlException )
            {
            }
            catch ( System.InvalidOperationException )
            {
            }

        }

    }
}