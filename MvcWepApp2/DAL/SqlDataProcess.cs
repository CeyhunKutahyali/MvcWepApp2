using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcWepApp2.DAL
{
    public class SqlDataProcess : IDisposable
    {
        SqlConnection conn;
        SqlCommand cmd;

        public SqlDataProcess()
        {
            conn = new SqlConnection(ConnectionString.Connection);
        }

        private void SqlConnectionOpen()
        {
            if (ConnectionState.Open != conn.State)
                conn.Open();

        }
        private void SqlConnectionClose()
        {
            if (ConnectionState.Open == conn.State)
                conn.Close();
        }

        public DataTable GetExecuteDataTable(string _cmd, CommandType cmdType)
        {
            using (cmd = new SqlCommand(_cmd, conn))
            {
                SqlConnectionOpen();
                DataTable dt = new DataTable();
                cmd.CommandType = cmdType;
                dt.Load(cmd.ExecuteReader());
                SqlConnectionClose();
                return dt;
            }
        }
        public DataTable GetExecuteDataTable(string _cmd, CommandType cmdType, params SqlParameter[] sqlParams)
        {
            using (cmd = new SqlCommand(_cmd, conn))
            {
                SqlConnectionOpen();
                DataTable dt = new DataTable();
                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(sqlParams);
                dt.Load(cmd.ExecuteReader());
                SqlConnectionClose();
                return dt;
            }
        }

        public bool SetExecuteNonQuery(string _cmd, CommandType cmdType)
        {
            try
            {
                using (cmd = new SqlCommand(_cmd, conn))
                {
                    SqlConnectionOpen();
                    cmd.CommandType = cmdType;
                    cmd.ExecuteNonQuery();
                    SqlConnectionClose();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SetExecuteNonQuery(string _cmd, CommandType cmdType, params SqlParameter[] sqlParams)
        {
            try
            {
                using (cmd = new SqlCommand(_cmd, conn))
                {
                    SqlConnectionOpen();
                    cmd.CommandType = cmdType;
                    cmd.Parameters.AddRange(sqlParams);
                    cmd.ExecuteNonQuery();
                    SqlConnectionClose();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public void Dispose()
        {
            SqlConnectionClose();
        }
    }
}