using MySql.Data.MySqlClient;
using System;

namespace StudentREgistationsystem.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionstring;

        public DatabaseHelper(string server = "localhost", string database = "studentregistrationsystem", string username = "root", string password = "kkkkkk")
        {
            _connectionstring = $"server={server}; Database ={database}; Username ={username}; Password ={password};";
            TestConnection();
        }

        private void TestConnection()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionstring))
                {
                   connection.Open();
                    Console.WriteLine("Connection successful.");  
                }
            }
            catch (Exception ex)
            {
                
               Console.WriteLine($"Connection failed: {ex.Message}");
                throw;
            }
        }

          public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionstring);
        }
    
    }
   
}
