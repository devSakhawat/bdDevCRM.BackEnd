//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.Common;
//using System.Data.OleDb;
//using System.Data.OracleClient;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Azolution.Common.DataService;


//namespace Utilities
//{
//    public class CommonConnection
//    {
//        private CommonDbHelper oracleDbHelper = null;
//        private DBHelper SqlDbHelper = null;
//        private OdbcDbHelper MySqlDbHelper = null;
//        private string ConnectionType = "";
//        public DatabaseType DatabaseType { get; set; }
//        public bool IsConfigureDb { get; set; }
//        private string ConnectionString = "";
//        // private OracleDbHelper oracleDbHelper11g = null;
//        public DatabaseProvider Provider { get; set; }
//        private IDataReader reader = null;
//        private bool isDbConnection { get; set; }
//        //public OracleConnection OracleConnection = null;
//        public SqlConnection SqlConnection = null;

//        //CommonConnection 

//        private DbCommand dbCommand = null;
//        private DbConnection dbConnection = null;
//        private IsolationLevel rIsolationLevel;
//        private bool isIsolation { get; set; }
//        private DbTransaction dbTransaction = null;

//        public CommonConnection()
//        {
//            if (ConfigurationSettings.AppSettings != null)
//            {
//                var connectionType = ConfigurationSettings.AppSettings["DataBaseType"];
//                if (!string.IsNullOrEmpty(connectionType))
//                {
//                    ConnectionType = connectionType.Trim();
//                }
//            }
//            if (ConnectionType != null && ConnectionType == "SQL")
//            {
//                ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
//                ConnectionString = "SqlConnectionString";
//                this.DatabaseType = DatabaseType.SQL;
//                SqlDbHelper = new DBHelper(ConnectionString);
//                SqlConnection = new SqlConnection(connections[ConnectionString].ConnectionString);
//                IsConfigureDb = true;
//            }
//            else if (ConnectionType != null && ConnectionType == "MySql")
//            {
//                ConnectionString = "MySqlConnectionString";
//                this.DatabaseType = DatabaseType.MySql;
//                MySqlDbHelper = new OdbcDbHelper(ConnectionString);
//                IsConfigureDb = true;
//            }
//            else if (ConnectionType != null && ConnectionType == "Oracle")
//            {
//                ConnectionString = "OracleConnectionString";
//                ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
//                this.DatabaseType = DatabaseType.Oracle;
//                oracleDbHelper = new CommonDbHelper(ConnectionString);
//                //OracleConnection = new OracleConnection(connections[ConnectionString].ConnectionString);
//                IsConfigureDb = true;
//                //dbCommand = oracleDbHelper.objSqlCommand;
//            }
//            else
//            {
//                IsConfigureDb = false;
//            }
//        }
//        public CommonConnection(string connectionString, DatabaseType dbType)
//        {

//            var connectionType = dbType.ToString();
//            if (!string.IsNullOrEmpty(connectionType))
//            {
//                ConnectionType = connectionType.Trim();
//            }
//            if (ConnectionType != null && ConnectionType == "SQL")
//            {
//                ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
//                ConnectionString = connectionString;
//                this.DatabaseType = DatabaseType.SQL;
//                SqlDbHelper = new DBHelper(connectionString);
//                SqlConnection = new SqlConnection(connections[connectionString].ConnectionString);
//                IsConfigureDb = true;
//            }
//            else if (ConnectionType != null && ConnectionType == "MySql")
//            {
//                // ConnectionString = "MySqlConnectionString";
//                this.DatabaseType = DatabaseType.MySql;
//                MySqlDbHelper = new OdbcDbHelper(ConnectionString);
//                IsConfigureDb = true;
//            }
//            else if (ConnectionType != null && ConnectionType == "Oracle")
//            {
//                //ConnectionString = "OracleConnectionString";
//                ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
//                this.DatabaseType = DatabaseType.Oracle;
//                oracleDbHelper = new CommonDbHelper(connections[connectionString].ConnectionString);
//                //OracleConnection = new OracleConnection(connections[ConnectionString].ConnectionString);
//                IsConfigureDb = true;
//                //dbCommand = oracleDbHelper.objSqlCommand;
//            }
//            else
//            {
//                IsConfigureDb = false;
//            }
//        }

//        public CommonConnection(string oleDbDataSource)
//        {
//            var xlConString = ConfigurationManager.AppSettings["XLConnectionString"];
//            //string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + odbcDataSource + ";Extended Properties=Excel 12.0;";
//            string strConn = string.Format(xlConString, oleDbDataSource);

//            dbConnection = new OleDbConnection();
//            dbConnection.ConnectionString = strConn;
//            DatabaseType = DatabaseType.OleDb;
//        }

//        public CommonConnection(IsolationLevel readCommitted)
//        {
//            rIsolationLevel = readCommitted;
//            isIsolation = true;

//            if (ConfigurationSettings.AppSettings != null)
//            {
//                var connectionType = ConfigurationSettings.AppSettings["DataBaseType"];
//                if (!string.IsNullOrEmpty(connectionType))
//                {
//                    ConnectionType = connectionType.Trim();
//                }
//            }
//            if (ConnectionType != null && ConnectionType == "SQL")
//            {
//                ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
//                ConnectionString = "SqlConnectionString";
//                this.DatabaseType = DatabaseType.SQL;
//                dbConnection = new SqlConnection(connections[ConnectionString].ConnectionString);
//                IsConfigureDb = true;
//            }

//            else if (ConnectionType != null && ConnectionType == "Oracle")
//            {
//                ConnectionString = "OracleConnectionString";
//                ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
//                this.DatabaseType = DatabaseType.Oracle;
//                dbConnection = new OracleConnection(connections[ConnectionString].ConnectionString);

//                IsConfigureDb = true;
//            }
//            else
//            {
//                IsConfigureDb = false;
//            }
//        }

//        public CommonConnection(bool isDbConnection)
//        {
//            this.isDbConnection = isDbConnection;


//            if (ConfigurationSettings.AppSettings != null)
//            {
//                var connectionType = ConfigurationSettings.AppSettings["DataBaseType"];
//                if (!string.IsNullOrEmpty(connectionType))
//                {
//                    ConnectionType = connectionType.Trim();
//                }
//            }
//            if (ConnectionType != null && ConnectionType == "SQL")
//            {
//                ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
//                ConnectionString = "SqlConnectionString";
//                this.DatabaseType = DatabaseType.SQL;
//                dbConnection = new SqlConnection(connections[ConnectionString].ConnectionString);
//                IsConfigureDb = true;
//            }

//            else if (ConnectionType != null && ConnectionType == "Oracle")
//            {
//                ConnectionString = "OracleConnectionString";
//                ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
//                this.DatabaseType = DatabaseType.Oracle;
//                dbConnection = new OracleConnection(connections[ConnectionString].ConnectionString);

//                IsConfigureDb = true;
//            }
//            else
//            {
//                IsConfigureDb = false;
//            }
//        }


//        //public CommonConnection(string sql)
//        //{
//        //    //GetDataReader(sql);
//        //}
//        public IDataReader GetDataReader(string sql)
//        {
//            var connectionString = "";

//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    //SqlDbHelper = new DBHelper(ConnectionString);
//                    reader = SqlDbHelper.GetDataReader(sql);
//                    break;
//                case DatabaseType.MySql:
//                    //MySqlDbHelper = new OdbcDbHelper(ConnectionString);
//                    reader = MySqlDbHelper.GetDataReader(sql);
//                    break;
//                case DatabaseType.Oracle:
//                    //oracleDbHelper = new CommonDbHelper(ConnectionString);
//                    reader = oracleDbHelper.GetDataReader(sql);
//                    //}
//                    break;
//            }
//            return reader;
//        }

//        public void ExecuteNonQuery(string sql)
//        {
//            if (isIsolation)
//            {
//                ExecuteDbCommand(sql);
//            }
//            else
//            {
//                switch (DatabaseType)
//                {
//                    case DatabaseType.SQL:
//                        //SqlDbHelper = new DBHelper(ConnectionString);
//                        SqlDbHelper.ExecuteNonQuery(sql);

//                        break;
//                    case DatabaseType.MySql:
//                        //MySqlDbHelper = new OdbcDbHelper(ConnectionString);
//                        MySqlDbHelper.ExecuteNonQuery(sql);
//                        break;
//                    case DatabaseType.Oracle:
//                        //oracleDbHelper = new CommonDbHelper(ConnectionString);
//                        oracleDbHelper.ExecuteNonQuery(sql);

//                        break;
//                }
//            }
//        }

//        public void ExecuteNonQuery(string sql, SQLParam[] param)
//        {
//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    //SqlDbHelper = new DBHelper(ConnectionString);
//                    SqlDbHelper.ExecuteNonQuery(sql, param);

//                    break;
//                case DatabaseType.MySql:
//                    //MySqlDbHelper = new OdbcDbHelper(ConnectionString);
//                    MySqlDbHelper.ExecuteNonQuery(sql, param);
//                    break;
//                case DatabaseType.Oracle:
//                    //oracleDbHelper = new CommonDbHelper(ConnectionString);
//                    oracleDbHelper.ExecuteNonQuery(sql, param);

//                    break;
//            }
//        }

//        public void RollBack()
//        {
//            if (isIsolation)
//            {
//                dbTransaction.Rollback();
//            }
//            else
//            {
//                switch (DatabaseType)
//                {
//                    case DatabaseType.SQL:
//                        SqlDbHelper.RollBack();
//                        break;
//                    case DatabaseType.MySql:
//                        MySqlDbHelper.RollBack();
//                        break;
//                    case DatabaseType.Oracle:
//                        //if (Provider == DatabaseProvider.ODT)
//                        //{
//                        oracleDbHelper.RollBack();
//                        //}
//                        //else
//                        //{
//                        //    oracleDbHelper.RollBack();
//                        //}
//                        break;
//                }
//            }
//        }

//        public void Close()
//        {
//            if (isIsolation)
//            {
//                dbCommand.Dispose();
//                dbTransaction.Dispose();
//                dbConnection.Close();
//            }
//            else
//            {
//                switch (DatabaseType)
//                {
//                    case DatabaseType.SQL:
//                        SqlDbHelper.Close();
//                        break;
//                    case DatabaseType.MySql:
//                        MySqlDbHelper.Close();
//                        break;
//                    case DatabaseType.Oracle:

//                        oracleDbHelper.Close();

//                        break;
//                }
//            }
//        }


//        public void Dispose()
//        {
//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    SqlDbHelper.Close();
//                    break;
//                case DatabaseType.MySql:
//                    MySqlDbHelper.Close();
//                    break;
//                case DatabaseType.Oracle:
//                    oracleDbHelper.Close();
//                    break;
//            }
//        }

//        public void BeginTransaction()
//        {
//            if (isIsolation)
//            {
//                if (dbConnection.State == ConnectionState.Closed)
//                {
//                    dbConnection.Open();
//                }

//                dbTransaction = dbConnection.BeginTransaction(rIsolationLevel);
//                dbCommand = dbConnection.CreateCommand();
//                dbCommand.Transaction = dbTransaction;
//            }
//            else
//            {
//                switch (DatabaseType)
//                {
//                    case DatabaseType.SQL:
//                        SqlDbHelper.BeginTransaction();
//                        break;
//                    case DatabaseType.MySql:
//                        MySqlDbHelper.BeginTransaction();
//                        break;
//                    case DatabaseType.Oracle:
//                        oracleDbHelper.BeginTransaction();
//                        break;
//                }
//            }
//        }

//        //public void BeginTransaction(IsolationLevel readCommitted)
//        //{
//        //    DbTransaction transaction;
//        //    ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;

//        //    dbConnection.Open();
//        //    transaction = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
//        //    dbCommand.Connection = dbConnection;
//        //    dbCommand.Transaction = transaction;


//        //}


//        public void CommitTransaction()
//        {
//            if (isIsolation)
//            {
//                dbTransaction.Commit();
//            }
//            else
//            {
//                switch (DatabaseType)
//                {
//                    case DatabaseType.SQL:
//                        SqlDbHelper.CommitTransaction();
//                        break;
//                    case DatabaseType.MySql:
//                        MySqlDbHelper.CommitTransaction();
//                        break;
//                    case DatabaseType.Oracle:
//                        oracleDbHelper.CommitTransaction();
//                        break;
//                }
//            }
//        }

//        public DataTable GetDataTable(string sql)
//        {
//            DataTable returnDT = null;
//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    returnDT = SqlDbHelper.GetDataTable(sql);
//                    break;
//                case DatabaseType.MySql:
//                    // MySqlDbHelper.GetTable(sql);
//                    break;
//                case DatabaseType.Oracle:
//                    returnDT = oracleDbHelper.GetTable(sql);

//                    break;
//            }
//            return returnDT;
//        }
//        public DataTable GetDataTables(string sqls)
//        {
//            DataTable returnDT = null;
//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    returnDT = SqlDbHelper.GetDataTable(sqls);
//                    break;
//                case DatabaseType.MySql:
//                    // MySqlDbHelper.GetTable(sql);
//                    break;
//                case DatabaseType.Oracle:
//                    returnDT = oracleDbHelper.GetTable(sqls);

//                    break;
//            }
//            return returnDT;
//        }

//        public DataSet GetDataSet(string sql)
//        {
//            DataSet returnDT = null;
//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    returnDT = SqlDbHelper.GetDataSet(sql);
//                    break;
//                case DatabaseType.MySql:
//                    // MySqlDbHelper.GetTable(sql);
//                    break;
//                case DatabaseType.Oracle:
//                    returnDT = oracleDbHelper.GetDataSet(sql);

//                    break;
//            }
//            return returnDT;
//        }

//        public DataTable GetDataTable(string sql, int minute)
//        {
//            DataTable returnDT = new DataTable();

//            SqlConnection com = new SqlConnection(dbConnection.ConnectionString);
//            SqlCommand cmd = new SqlCommand(sql, com);
//            cmd.CommandTimeout = minute * 60 * 1000;
//            cmd.CommandType = CommandType.Text;
//            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

//            adapter.Fill(returnDT);


//            return returnDT;
//        }


//        public int GetScaler(string sql)
//        {
//            int returnCount = 0;
//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    returnCount = Convert.ToInt32(SqlDbHelper.GetScaler(sql));
//                    break;
//                case DatabaseType.MySql:
//                    //MySqlDbHelper.(sql);
//                    break;
//                case DatabaseType.Oracle:
//                    returnCount = Convert.ToInt32(oracleDbHelper.ExecuteScalar(sql));

//                    break;
//            }
//            return returnCount;
//        }

//        public object GetScalerObject(string sql)
//        {
//            object returnCount = null;
//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    // returnCount = Convert.ToInt32(SqlDbHelper.GetScaler(sql));
//                    returnCount = SqlDbHelper.GetScaler(sql);
//                    break;
//                case DatabaseType.MySql:
//                    //MySqlDbHelper.(sql);
//                    break;
//                case DatabaseType.Oracle:
//                    returnCount = oracleDbHelper.ExecuteScalar(sql);

//                    break;
//            }
//            return returnCount;
//        }

//        public List<T> Data<T>(string query)
//        {
//            DataTable dataTable = GetDataTable(query);

//            var objData = (List<T>)ListConversion.ConvertTo<T>(dataTable);
//            return objData;
//        }

//        public List<T> Data<T>(string query, int minute)
//        {
//            DataTable dataTable = GetDataTable(query, minute);

//            var objData = (List<T>)ListConversion.ConvertTo<T>(dataTable);
//            return objData;
//        }


//        public List<T> GenericDataSource<T>(string query)
//        {
//            DataTable dataTable = GetDataTable(query);

//            var objData = (List<T>)GenericListGenerator.GetList<T>(dataTable);
//            return objData;
//        }

//        public int ExecuteAfterReturnId(string query, string columnName)
//        {
//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    return ExcuteSqlAfterReturn(query);
//                    break;
//                case DatabaseType.MySql:
//                    return 0;
//                    break;
//                case DatabaseType.Oracle:
//                    return ExcuteOracleAfterReturn(query, columnName);
//                    break;
//                default:
//                    return 0;
//            }
//        }


//        private int ExcuteSqlAfterReturn(string query)
//        {
//            int rvId = 0;

//            try
//            {
//                //dbCommand = dbConnection.CreateCommand();
//                //dbCommand.Transaction = dbTransaction;
//                if (dbConnection.State == ConnectionState.Closed)
//                {
//                    dbConnection.Open();
//                }
//                dbCommand.CommandText = query + " select @@identity outId ";

//                var data = dbCommand.ExecuteScalar();


//                if (data != null)
//                {
//                    rvId = Convert.ToInt32(data);
//                }
//                else
//                {
//                    throw new Exception("Return value is null");
//                }
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//            return rvId;
//        }

//        //private int ExcuteOracleAfterReturn1(string query, string columnName)
//        //{
//        //    int rvId = 0;
//        //    ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
//        //    using (OracleConnection connection = new OracleConnection(connections[ConnectionString].ConnectionString))
//        //    {

//        //        connection.Open();

//        //        OracleCommand command = connection.CreateCommand();
//        //        OracleTransaction transaction;

//        //        // Start a local transaction.
//        //        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

//        //        // Must assign both transaction object and connection
//        //        // to Command object for a pending local transaction
//        //        command.Connection = connection;
//        //        command.Transaction = transaction;

//        //        try
//        //        {
//        //            command.CommandText = query + " returning " + columnName + " into :outId";

//        //            OracleParameter p2 = new OracleParameter("outId", OracleType.Int32);
//        //            p2.Direction = ParameterDirection.Output;
//        //            command.Parameters.Add(p2);
//        //            command.ExecuteNonQuery();
//        //            transaction.Commit();
//        //            rvId = Convert.ToInt32(command.Parameters[0].Value);

//        //        }
//        //        catch (Exception e)
//        //        {
//        //            try
//        //            {
//        //                transaction.Rollback();
//        //            }
//        //            catch (OracleException ex)
//        //            {
//        //                throw new Exception(ex.Message);

//        //            }
//        //            finally
//        //            {


//        //            }

//        //        }
//        //    }
//        //    return rvId;
//        //}

//        private int ExcuteOracleAfterReturn(string query, string columnName)
//        {
//            int rvId = 0;

//            try
//            {
//                query = query + " returning " + columnName + " into :outId";

//                DbParameter p2 = new OracleParameter("outId", OracleType.Int32);
//                p2.Direction = ParameterDirection.Output;
//                dbCommand = dbConnection.CreateCommand();
//                dbCommand.Transaction = dbTransaction;
//                dbCommand.CommandText = query;
//                dbCommand.Parameters.Add(p2);
//                dbCommand.ExecuteNonQuery();
//                //dbTransaction.Commit();

//                rvId = Convert.ToInt32(dbCommand.Parameters[0].Value);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//            return rvId;
//        }

//        private int ExcuteSqlAfterReturn(string query, string columnName)
//        {
//            int rvId = 0;

//            try
//            {
//                query = query + " select @@identity as outId ";

//                DbParameter p2 = new SqlParameter("outId", SqlDbType.Int);
//                p2.Direction = ParameterDirection.ReturnValue;

//                dbCommand = dbConnection.CreateCommand();
//                dbCommand.Transaction = dbTransaction;
//                dbCommand.Parameters.Add(p2);
//                dbCommand.CommandText = query;
//                dbCommand.ExecuteNonQuery();
//                //dbTransaction.Commit();

//                rvId = Convert.ToInt32(dbCommand.Parameters[0].Value);
//            }
//            catch (Exception e)
//            {
//                throw new Exception(e.Message);
//            }
//            return rvId;
//        }

//        private void ExecuteDbCommand(string sqlTxt)
//        {
//            dbCommand.CommandText = sqlTxt;
//            dbCommand.Connection = dbConnection;
//            dbCommand.CommandTimeout = 30000;
//            if (dbConnection.State == ConnectionState.Closed)
//                dbConnection.Open();
//            dbCommand.ExecuteNonQuery();
//        }
////Woeking
//        public DataTable GetXLDataTable()
//        {
//            if (dbConnection.State == ConnectionState.Closed)
//            {
//                dbConnection.Open();
//            }
//            dbCommand = new OleDbCommand("SELECT * FROM [Sheet1$]");
//            dbCommand.Connection = dbConnection;
//            DataTable dt = new DataTable();
//            dt.Load(dbCommand.ExecuteReader());
//            dbConnection.Close();
//            return dt;
//        }

//        /// <summary>
//        /// Get XL DATA TABLE OF SPECIFIC SHEET NAME
//        /// </summary>
//        /// <param name="sheetName">SHEET NAME</param>
//        /// <returns></returns>
//        //public DataTable GetXLDataTable(string sheetName)
//        //{
//        //    OleDbCommand com = new OleDbCommand();
//        //    if (dbConnection.State == ConnectionState.Closed)
//        //    {
//        //       // dbConnection.Close();
//        //        dbConnection.Open();
                
//        //    }
//        //    dbCommand = new OleDbCommand("SELECT * FROM [" + sheetName + "]");
//        //    dbCommand.Connection = dbConnection;
//        //    DataTable dt = new DataTable();
//        //    try
//        //    {
//        //        dt.Load(dbCommand.ExecuteReader());
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //    dbConnection.Close();
//        //    return dt;
//        //}


//        public DataTable GetXLDataTable(string sheetName)
//        {
//            if (dbConnection.State == ConnectionState.Closed)
//            {
//                dbConnection.Open();
//            }
//            //dbCommand = new OleDbCommand("SELECT * FROM [" + sheetName + "$]");
//            dbCommand = new OleDbCommand("SELECT * FROM [" + sheetName + "]");
//            dbCommand.Connection = dbConnection;
//            DataTable dt = new DataTable();
//            try
//            {
//                dt.Load(dbCommand.ExecuteReader());
//            }
//            catch (Exception)
//            {
//                dbCommand = new OleDbCommand("SELECT * FROM [" + sheetName + "$]");
//                try
//                {
//                    dbCommand.Connection = dbConnection;
//                    dt.Load(dbCommand.ExecuteReader());
//                }
//                catch (Exception)
//                {

//                    throw;
//                }
//            }
//            finally
//            {
//                dbConnection.Close();
//            }
//            return dt;
//        }
//        public DataTable GetXLDataTableWithSymbol(string sheetName)
//        {
//            OleDbCommand com = new OleDbCommand();
//            if (dbConnection.State == ConnectionState.Closed)
//            {
//                // dbConnection.Close();
//                dbConnection.Open();

//            }
//            dbCommand = new OleDbCommand("SELECT * FROM [" + sheetName + "$]");
//            dbCommand.Connection = dbConnection;
//            DataTable dt = new DataTable();

//            dt.Load(dbCommand.ExecuteReader());
//            dbConnection.Close();
//            return dt;
//        }

//        public DataTable GetXLDataTable(string sheetName, int userId)
//        {
//            if (dbConnection.State == ConnectionState.Closed)
//            {
//                dbConnection.Open();
//            }
//            dbCommand = new OleDbCommand(string.Format("SELECT *,{0} as UserId FROM [{1}$]", userId, sheetName));
//            dbCommand.Connection = dbConnection;
//            DataTable dt = new DataTable();
//            dt.Load(dbCommand.ExecuteReader());
//            dbConnection.Close();
//            return dt;
//        }

//        public Decimal GetScalerAmount(string sql)
//        {
//            decimal returnCount = 0;
//            switch (DatabaseType)
//            {
//                case DatabaseType.SQL:
//                    returnCount = Convert.ToDecimal(SqlDbHelper.GetScaler(sql));
//                    break;
//                case DatabaseType.MySql:
//                    //MySqlDbHelper.(sql);
//                    break;
//                case DatabaseType.Oracle:
//                    returnCount = Convert.ToDecimal(oracleDbHelper.ExecuteScalar(sql));

//                    break;
//            }
//            return returnCount;
//        }

//        public void ExecuteNonQuery(string query, List<SqlParameter> sqlParams)
//        {
//            try
//            {
//                if (dbConnection == null)
//                    dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);

//                if (dbCommand == null)
//                    dbCommand = dbConnection.CreateCommand();
//                dbCommand.CommandText = query;
//                dbCommand.Connection = dbConnection;
//                dbCommand.Parameters.AddRange(sqlParams.ToArray());
//                if (dbConnection.State == ConnectionState.Closed)
//                    dbConnection.Open();
//                dbCommand.ExecuteNonQuery();
//            }
//            catch (Exception ex)
//            {
//                throw ex;

//            }
//            finally
//            {
//                if (dbConnection.State == ConnectionState.Open)
//                    dbConnection.Close();
//            }

//        }
//    }
//}