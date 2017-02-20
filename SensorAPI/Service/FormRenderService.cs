using SensorIOT.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SensorAPI.Service
{
    public class FormRenderService
    {
        public FormRenderService() { }
        /// <summary>
        /// This will inserts data in database
        /// </summary>
        /// <param name="execType">1 for simple inline Query and 2 for Store procedure</param>
        /// <param name="parameters">List of parameters required to insert</param>
        /// <returns></returns>
        public void Fetch(int execType, Dictionary<string, string> parameters)
        {
            if (execType == 1)
                ExecuteSingleData(parameters);
            else if (execType == 2)
                ExecuteDataProc(parameters);
        }

        /// <summary>
        /// Insert data with simple insert command.
        /// </summary>
        /// <param name="parameters">List of parameters required to insert</param>
        /// <returns></returns>
        public void ExecuteSingleData(Dictionary<string, string> parameters)
        {
            string command = "insert into EmployeeMaster(FirstName, LastName,FullName, Age, City, State, PhoneNo) Values(@firstName,@surname,@fullName,@age,@city,@state,@phoneNo)";
            Task.Run(() => ExecuteData(command, parameters, CommandType.Text));
        }

        /// <summary>
        /// Insert data with command type as Store procedure.
        /// </summary>
        /// <param name="parameters">List of parameters required to insert</param>
        /// <returns></returns>
        public void ExecuteDataProc(Dictionary<string, string> parameters)
        {
            string command = "CreateEmployeeMaster";
            Task.Run(() => ExecuteData(command, parameters, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Execute data with command and parameters with command type
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public void ExecuteData(string command, Dictionary<string, string> parameters, CommandType commandType)
        {
            Stopwatch innerWatch = new Stopwatch();
            innerWatch.Start();

            string connectionString = ConfigurationManager.ConnectionStrings["AppConnection"].ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(command, con) { CommandType = commandType })
                    {
                        foreach (var param in parameters)
                        {
                            sqlCommand.Parameters.Add(new SqlParameter(param.Key, param.Value));
                        }

                        sqlCommand.ExecuteNonQuery();
                    }
                    innerWatch.Stop();
                    AppLogger.LogTimer(innerWatch);
                }
                catch (Exception ex)
                {
                    innerWatch.Stop();
                    AppLogger.LogError(ex);
                }
                finally
                {
                    if (con.State != ConnectionState.Closed)
                        con.Close();
                    innerWatch.Stop();
                }
            }
        }

    }
}