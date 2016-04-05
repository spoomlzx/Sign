using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace Sign.util
{
    public class SqliteHelper
    {
        private SQLiteConnection conn = null;
        public SqliteHelper()
        {
            conn = CreateConnection();
            OpenConnection();
        }

        ~SqliteHelper()
        {
            CloseConnection();
        }

        private SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlconn = null;
            try
            {
                sqlconn = new SQLiteConnection("Data Source=db.db");
            }
            catch (Exception)
            {
                SQLiteConnection.CreateFile("db.db");
                sqlconn = new SQLiteConnection("Data Source=db.db");
            }
            return sqlconn;
        }

        private bool OpenConnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Open();
                }
                else
                {
                    conn = CreateConnection();
                    conn.Open();
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        #region Select,Execute
        public DataTable Select(string sql)
        {
            return Select(sql, new List<SQLiteParameter>());
        }

        public DataTable Select(string sql,List<SQLiteParameter> cmdparams)
        {
            DataTable dt = new DataTable();
            using (SQLiteCommand mycommand = new SQLiteCommand(conn))
            {
                mycommand.CommandText = sql;
                mycommand.Parameters.AddRange(cmdparams.ToArray());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(mycommand);
                adapter.Fill(dt);
            }
            return dt;
        }

        public void Execute(string sql)
        {
            Execute(sql, new List<SQLiteParameter>());
        }

        public void Execute(string sql, List<SQLiteParameter> cmdparams)
        {
            using (SQLiteCommand mycommand = new SQLiteCommand(conn))
            {
                mycommand.CommandText = sql;
                mycommand.Parameters.AddRange(cmdparams.ToArray());
                mycommand.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, new List<SQLiteParameter>());
        }

        public object ExecuteScalar(string sql, List<SQLiteParameter> cmdparams)
        {
            Object obj = null;
            using (SQLiteCommand mycommand = new SQLiteCommand(conn))
            {
                mycommand.CommandText = sql;
                mycommand.Parameters.AddRange(cmdparams.ToArray());
                obj = mycommand.ExecuteScalar();
            }
            return obj;
        }
        #endregion

        public DataTable GetSchema()
        {
            DataTable data = conn.GetSchema("TABLES");
            return data;
        }

    }
}
