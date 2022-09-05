//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace CAPI.DAL.DBConnection
//{
//    class DapperHelper
//    {
//    }
//}


using Dapper;
using Microsoft.Extensions.Configuration;
using CAPI.CONTRACT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPI.DAL.DBConnection
{
    public class DapperHelper : IDapperHelper
    {
        protected IDbConnection con;
        public DynamicParameters param = null;
        private bool boolHandleErrors;
        private string strLastError;
        private bool boolLogError;

        private readonly IConfiguration _config;

        public DapperHelper(IConfiguration config)
        {
            _config = config;
            con = new SqlConnection(_config.GetSection("Database").GetSection("connectionstring").Value.ToString());
            param = new DynamicParameters();
        }

        public bool HandleErrors
        {
            get
            {
                return boolHandleErrors;
            }
            set
            {
                boolHandleErrors = value;
            }
        }
        public bool LogErrors
        {
            get
            {
                return boolLogError;
            }
            set
            {
                boolLogError = value;
            }
        }

        private void HandleExceptions(Exception ex)
        {
            if (LogErrors)
            {
                WriteToLog(ex);
            }
            if (HandleErrors)
            {
                strLastError = ex.Message;
            }
            else
            {
                //throw ex;
            }
        }
        private void WriteToLog(Exception ex)
        {
            //StreamWriter writer;
            ////string strFilePath = ConfigurationManager.AppSettings["errorLogFilePath"].ToString();
            //LogFile += "\\Log_" + DateTime.Today.Date.ToString("dd.MMM.yyyy", DateTimeFormatInfo.InvariantInfo) + ".txt";
            //LogFile = HttpContext.Current.Request.PhysicalApplicationPath + LogFile;
            //if(!File.Exists(LogFile))
            //    writer = File.CreateText(LogFile);
            //else
            //    writer = File.AppendText(LogFile);
            //writer.WriteLine("<------------> Database Exception Starts <-------------------->");
            ////writer.WriteLine(" Logged In By : " + HttpContext.Current.Session["loginname"].ToString());
            //writer.WriteLine(" User Session ID : " + HttpContext.Current.Session.SessionID.ToString());
            //writer.WriteLine(" Browser Information : " + HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString());
            //writer.WriteLine(ex.InnerException.Message);
            //writer.WriteLine(ex.InnerException.StackTrace);
            //writer.WriteLine("<---------------------> Database Exception Ends <----------------->");
            //writer.Close();
        }


        public void AddParameter(string name, object value)
        {
            param.Add(name, value);
        }
        public void ClearParameter()
        {
            param = new DynamicParameters();
        }

        public void AddParameter(string name, object value, System.Data.DbType type)
        {
            param.Add(name, value, type);
        }

        public void AddParameter(string name, object value, System.Data.DbType type = DbType.String, ParameterDirection pd = ParameterDirection.Input)
        {
            param.Add(name, value, type, direction: pd);
        }




        public string ExecuteNonQuery(string query, CommandType cmdtype)
        {
            try
            {
                int intRowsAffected = 0;
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                intRowsAffected = SqlMapper.Execute(con, query, param, commandType: cmdtype);

                return "{\"result\": [{\"RowsAffected\": " + intRowsAffected.ToString() + "},]}";
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                return "-4 : Error --------> " + ex.Message + Environment.NewLine + "< ------- >StackTrace --------> " + ex.StackTrace;
            }
            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public string ExecuteNonQuery<T>(string query, T obj, CommandType cmdtype)
        {
            try
            {
                int intRowsAffected = 0;
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                intRowsAffected = SqlMapper.Execute(con, query, param: obj, commandType: cmdtype);

                return "{\"result\": [{\"RowsAffected\": " + intRowsAffected.ToString() + "},]}";
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                return "-4 : Error --------> " + ex.Message + Environment.NewLine + "< ------- >StackTrace --------> " + ex.StackTrace;
            }
            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        public string ExecuteNonQuery(string query, CommandType cmdtype, string outputparam)
        {
            string outputParamValue = "";
            try
            {
                int intRowsAffected = 0;
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                intRowsAffected = SqlMapper.Execute(con, query, param, commandType: cmdtype);
                outputParamValue = param.Get<string>(outputparam);
                return "{\"result\": [{\"RowsAffected\": " + Convert.ToString(outputParamValue) + "},]}";
            }
            catch (Exception ex)
            {
                return "-4 : Error --------> " + ex.Message + Environment.NewLine + "< ------- >StackTrace --------> " + ex.StackTrace;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public string ExecuteNonQueryReturnID(string query, CommandType cmdtype, string outputparam)
        {
            string outputParamValue = "";
            try
            {
                int intRowsAffected = 0;
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                intRowsAffected = SqlMapper.Execute(con, query, param, commandType: cmdtype);
                outputParamValue = Convert.ToString(param.Get<Int32>(outputparam));
                return outputParamValue;
            }
            catch (Exception ex)
            {
                return "-4 : Error --------> " + ex.Message + Environment.NewLine + "< ------- >StackTrace --------> " + ex.StackTrace;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        public string ExecuteScalar(string query, CommandType cmdType)
        {

            try
            {
                string strRowsAffected = "0";

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                strRowsAffected = Convert.ToString(SqlMapper.ExecuteScalar(con, query, param, commandType: cmdType));

                return Convert.ToString("{\"result\": [{\"RowsAffected\": " + Convert.ToString(strRowsAffected) + "},]}");
            }
            catch (Exception ex)
            {
                return "-4 : Error --------> " + ex.Message + Environment.NewLine + "< ------- >StackTrace --------> " + ex.StackTrace;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        public string GetDataJson<T>(string query, CommandType cmdType)
        {
            List<T> list = new List<T>();
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                list = SqlMapper.Query<T>(con, query, param, commandType: cmdType).ToList();
                return list.Count > 0 ? Newtonsoft.Json.JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.None) : "-9";
            }
            catch (Exception ex)
            {
                return "-4 : Error --------> " + ex.Message + Environment.NewLine + "< ------- >StackTrace --------> " + ex.StackTrace;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public List<T> GetDataList<T>(string query, CommandType cmdType)
        {
            List<T> list = new List<T>();
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                list = SqlMapper.Query<T>(con, query, param, commandType: cmdType).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        public string GetRecordJsonByID<T>(string query, CommandType cmdType)
        {
            T obj = default(T);
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                obj = SqlMapper.Query<T>(con, query, param, commandType: cmdType).SingleOrDefault();
                return obj != null ? Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented) : "-9";
            }
            catch (Exception ex)
            {
                return "-4 : Error --------> " + ex.Message + Environment.NewLine + "< ------- >StackTrace --------> " + ex.StackTrace;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        public T GetRecordByID<T>(string query, CommandType cmdType)
        {
            T obj = default(T);
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                obj = SqlMapper.Query<T>(con, query, param, commandType: cmdType).SingleOrDefault();
                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        public string ExecuteNonQueryAng(string query, CommandType cmdtype)
        {
            try
            {
                int intRowsAffected = 0;
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                intRowsAffected = SqlMapper.Execute(con, query, param, commandType: cmdtype);

                return Convert.ToString(intRowsAffected);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
                return "-4 : Error --------> " + ex.Message + Environment.NewLine + "< ------- >StackTrace --------> " + ex.StackTrace;
            }
            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        public void Dispose()
        {
            con.Close();
            con.Dispose();
        }
    }
}
