using System;
using System.Data.SqlClient;

namespace SqlDatabase
{
    public class Database
    {
        public SqlConnection Connect()
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = "Data Source=TAVDESK060;Initial Catalog=TicTacDB;Integrated Security=True";
                return connection;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return null;
            }
        }

        public bool InsertUsers(string firstName, string lastName, string userName, string accessToken)
        {
            SqlConnection connection = null;
            try
            {
                connection = Connect();
                connection.Open();
                string query = "Insert into Users(FirstName,LastName,Username,AccessToken) values(@firstName,@lastName,@userName,@accessToken)";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.Add(new SqlParameter("firstName", firstName));
                sqlCommand.Parameters.Add(new SqlParameter("lastName", lastName));
                sqlCommand.Parameters.Add(new SqlParameter("userName", userName));
                sqlCommand.Parameters.Add(new SqlParameter("accessToken", accessToken));
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return false;
            }
        }
        public bool InsertLog(string requestContent, string responseContent, string exceptionContent, string comment)
        {
            SqlConnection connection = null;
            try
            {
                connection = Connect();
                connection.Open();
                string query = "Insert into LogData(RequestContent, ResponseContent, ExceptionContent, Comment) values(@requestContent, @responseContent, @exceptionContent, @comment)";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.Add(new SqlParameter("requestContent", requestContent));
                sqlCommand.Parameters.Add(new SqlParameter("responseContent", responseContent));
                sqlCommand.Parameters.Add(new SqlParameter("exceptionContent", exceptionContent));
                sqlCommand.Parameters.Add(new SqlParameter("comment", comment));
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return false;
            }
        }
        public bool CheckAccessTokenExist(string accessToken)
        {
            SqlConnection connection = null;
            try
            {
                connection = Connect();
                connection.Open();
                string query = "Select * from Users where AccessToken=@accessToken";
                SqlCommand sqlCommand = new SqlCommand(query,connection);
                sqlCommand.Parameters.Add(new SqlParameter("@accessToken", accessToken));
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                bool flag = false;
                if (sqlDataReader.Read())
                    flag = true;
                connection.Close();
                return flag;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return false;
            }
        }
        public string GetUsername(string accessToken)
        {
            SqlConnection connection = null;
            try
            {
                string username="";
                connection = Connect();
                connection.Open();
                string query = "Select * from Users where AccessToken=@accessToken";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.Add(new SqlParameter("@accessToken", accessToken));
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                    username = sqlDataReader[3].ToString();
                connection.Close();
                return username;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
                return "";
            }
        }
    }
}
