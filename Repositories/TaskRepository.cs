using System;
using System;
using StudentREgistationsystem.Data;
using StudentREgistationsystem.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Task = StudentREgistationsystem.Models.Task;
 namespace StudentREgistationsystem.Repositories
 {
    public class TaskRepository
    {
        private readonly DatabaseHelper databaseHelper;

        public TaskRepository()
      {
         databaseHelper = new DatabaseHelper();
      }

      public int AddStudent(Task task)
      {
         const string query= @"
          
        INSERT INTO Tasks (Full_Name, student_Email, student_Age, student_Course) 
        VALUES (@Full_Name, @student_Email, @student_Age, @student_Course);
        SELECT LAST_INSERT_ID();"; // Use correct MySQL function

    using var connection = databaseHelper.GetConnection(); 
    using var command = new MySqlCommand(query, connection); 
    
    // Ensure C# parameter names match SQL placeholders exactly
        command.Parameters.AddWithValue("@Full_Name", task.FullName); 
    command.Parameters.AddWithValue("@student_Email", task.StudentEmail); 
    command.Parameters.AddWithValue("@student_Age", task.StudentAge); 
    command.Parameters.AddWithValue("@student_Course", task.StudentCourse); 
            connection.Open();
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result);
            
      }

       public List<Task> DisplayAllStudent()
      {
         var tasks = new List <Task>();
         const string query = "SElECT * FROM Tasks ";
         using var connection = databaseHelper.GetConnection();
         using var command = new MySqlCommand (query, connection);
         
         
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(MapReaderToTask(reader));
            }
            return tasks;
      }
       public bool UpdateStudent(Task task)
        {
             const string query= @"
        INSERT INTO Tasks (Full_Name, student_Email, student_Age, student_Course) 
        VALUES (@Full_Name, @student_Email, @student_Age, @student_Course);
        SELECT LAST_INSERT_ID();"; // Correct MySQL function

    // Assuming 'databaseHelper' is a valid way to get your connection
    using var connection = databaseHelper.GetConnection(); 
    using var command = new MySqlCommand(query, connection); 
    
    // Ensure C# parameter names exactly match the SQL placeholders AND the DB field names
    command.Parameters.AddWithValue("@Full_Name", task.FullName); 
    command.Parameters.AddWithValue("@student_Email", task.StudentEmail); 
    command.Parameters.AddWithValue("@student_Age", task.StudentAge); 
    command.Parameters.AddWithValue("@student_Course", task.StudentCourse); 

            connection.Open();
            var rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
         public bool DeleteStudent(int taskId)
        {
            const string query = "DELETE FROM Tasks WHERE student_id  = @student_id";

            using var connection = databaseHelper.GetConnection();
            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@student_id", taskId);

            connection.Open();
            var rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
         public Task GetStudentById(int taskId)
        {
            const string sql = "SELECT * FROM Tasks WHERE student_id = @student_id";
            
            using var connection = databaseHelper.GetConnection();
            using var command = new MySqlCommand(sql, connection);
            
            command.Parameters.AddWithValue("@student_id", taskId);
            
            connection.Open();
            using var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return MapReaderToTask(reader);
            }
            return null!;

        }  

            private Task MapReaderToTask(MySqlDataReader reader)
        {
            return new Task
            {
                StudentId= Convert.ToInt32(reader["student_id"]),
                FullName = reader["Full_Name"].ToString()!,
                 StudentCourse= reader["student_Course"].ToString()!,
                StudentEmail = reader["student_Email"].ToString()!,
                StudentAge= Convert.ToInt32(reader["Student_Age"])
            };
        }

    
  }
 }