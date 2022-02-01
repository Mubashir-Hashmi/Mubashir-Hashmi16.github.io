using Agilosoft.AgileTimeKeeper.Api.Errors;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using log4net;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace Agilosoft.AgileTimeKeeper.Api.Controllers
{
    public class ExecutorController
    {
        //private IConfiguration _configuration;
        //private string _sqlDataSource;
        private string connectionString;
        private SqlConnection _myConnection;
        public ExecutorController(IConfiguration configuration)
        {
            //_configuration = configurationParameter;
            //_sqlDataSource = _configuration.GetConnectionString("AgileTimeKeeperAppCon");
            connectionString = configuration.GetConnectionString("AgileTimeKeeperAppCon");
        }

        public  DataTable ExecuteQuery(string storedProcedureName, Object[] parameterArray, ILog log)
        {
            SqlDataReader queryResultReader=null;
                DataTable resultTable = new DataTable();
                using (_myConnection = new SqlConnection(connectionString))
                {

                    if (_myConnection.State != ConnectionState.Open)
                    {
                        _myConnection.Open();
                    }
                    using SqlCommand myCommand = new SqlCommand(storedProcedureName, _myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    for (int iterator = 0; iterator < parameterArray.Length; iterator += 2)
                    {
                        myCommand.Parameters.AddWithValue(parameterArray[iterator].ToString(), parameterArray[iterator + 1]);
                    }
                    queryResultReader = myCommand.ExecuteReader();
                    resultTable.Load(queryResultReader);
                    if (resultTable.Rows.Count == 0)
                    {
                        log.Info("No Records Returned by '"+storedProcedureName+"'");
                    }
                    queryResultReader.Close();
                }
                if (_myConnection.State == ConnectionState.Open)
                {
                    _myConnection.Close();
                }
                if (queryResultReader!=null)
                {
                    queryResultReader.Close();
                }
                return resultTable;
        }

        public string GetRole(string email)
        {
            SqlDataReader queryResultReader = null;
            DataTable resultTable = new DataTable();
            var result = "";
            using (_myConnection = new SqlConnection(connectionString))
            {

                if (_myConnection.State != ConnectionState.Open)
                {
                    _myConnection.Open();
                }
                using SqlCommand myCommand = new SqlCommand("usp_Chk_UserRole", _myConnection);
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@vcUserEmail", email);
                queryResultReader = myCommand.ExecuteReader();
                resultTable.Load(queryResultReader);
                if (resultTable.Rows.Count > 0)
                {
                    result = resultTable.Rows[0]["Role"].ToString();
                }
                queryResultReader.Close();
            }
            return result;
        }
        
        public DataTable ExecuteQuery(string storedProcedureName, ILog log)
        {
            SqlDataReader queryResultReader = null;
                DataTable resultTable = new DataTable();
                using (_myConnection = new SqlConnection(connectionString))
                {
                    if (_myConnection.State != ConnectionState.Open)
                    {
                        _myConnection.Open();
                    }
                    using SqlCommand myCommand = new SqlCommand(storedProcedureName, _myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    queryResultReader = myCommand.ExecuteReader();
                    resultTable.Load(queryResultReader);
                    if (resultTable.Rows.Count == 0)
                    {
                        log.Info("No Records Returned by '" + storedProcedureName + "'");
                    }  
                    queryResultReader.Close();
                }
                if (_myConnection.State == ConnectionState.Open)
                {
                    _myConnection.Close();
                }
                if (queryResultReader != null)
                {
                    queryResultReader.Close();
                }
                return resultTable;
        }

        public string SendEmailToUser(string Server, Int16 Port, Boolean IsSSL, string EmailFrom, string MessageFromName, string Password, string EmailTo, string MessageToName, string EmailBody, string EmailSubject, ILog log)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(MessageFromName, EmailFrom));
            message.To.Add(new MailboxAddress(MessageToName, EmailTo));
            message.Subject = EmailSubject;
            message.Body = new TextPart("plain")
            {
                Text = EmailBody
            };
                using (var client = new SmtpClient())
                {
                    client.Connect(Server, Port, IsSSL);
                    client.Authenticate(EmailFrom, Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
                return ("Email Sent");
        }
    }
}