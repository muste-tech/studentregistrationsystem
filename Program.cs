using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using StudentREgistationsystem.Models;
using StudentREgistationsystem.Repositories;
using Task = StudentREgistationsystem.Models.Task;
namespace StudentREgistationsystem
{
    public class Program
    {
        private static TaskRepository taskRepository = new TaskRepository();

        static void Main(string[] args)
        {
             Console.WriteLine(" Student registration system with ADO>NET & MySql!");
            Console.WriteLine("=============================================\n");
            
            ShowMainMenu();
        }
        static void ShowMainMenu()
        {
            while (true)
            {
                 Console.WriteLine("\n MAIN MENU");
                
                Console.WriteLine("1. Add new student");
                Console.WriteLine("2. View All students");
                Console.WriteLine("3. Update Existing student");
                Console.WriteLine("4. Delete a student");
                Console.WriteLine("5. Exit");
                Console.Write("\nChoose an option: ");
                 var choice = Console.ReadLine();
                    try
                {
                    switch (choice)
                    {
                        case "1":
                        AddAllStudent();
                        break;
                        case "2":
                        VeiwAllStudent();
                        break;
                        case "3":
                        UpdateExistingStudent();
                        break;
                        case "4":
                        DeleteAStudent();
                        break;
                        case "5":
                       Console.WriteLine("Goodbye! ");
                            return;   
                        default:
                        Console.WriteLine($"Invalid option. Press any key to continue... ");
                        Console.ReadKey();
                        break;




                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($" Error: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }   
        }

        static void AddAllStudent()
        {
            Console.Clear();
            Console.WriteLine("ADD NEW STUDENT\n");
            Console.WriteLine("FULLNAME: ");
             var full_Name = Console.ReadLine();
             if(string.IsNullOrWhiteSpace(full_Name))
            {
                Console.WriteLine("Title is required!");
                WaitForUser();
                return;
            }

             Console.WriteLine("Student Email: ");
             var student_Email = Console.ReadLine();
             if (string.IsNullOrWhiteSpace(student_Email))
             {
                Console.WriteLine("Email is required");
                WaitForUser();
                return;
             }
               Console.WriteLine("Student Course: ");
             var student_course = Console.ReadLine();
             if (string.IsNullOrWhiteSpace(student_course))
             {
                Console.WriteLine("Email is required");
                WaitForUser();
                return;
             }

               Console.WriteLine("Student age: ");
             var student_age = Console.ReadLine();
             if (!int.TryParse(student_age, out int student_Age ))
             {
                Console.WriteLine("age is required");
                WaitForUser();
                return;
             }
             var newStudent = new Models.Task
             {
                 FullName= full_Name,
                 StudentCourse = student_course,
                 StudentAge = student_Age,
                 StudentEmail = student_Email
             };

             var newId = taskRepository.AddStudent(newStudent);
          Console.WriteLine($"\n Task added successfully! ID: {newId}");
            WaitForUser();
       



        }
         static void VeiwAllStudent()
        {
            Console.Clear();
            Console.WriteLine(" ALL TASKS\n");

            var tasks = taskRepository.DisplayAllStudent();
           DisplayStudents (tasks);
            WaitForUser();
        }
        static void DisplayStudents(List<Task> tasks)
        {
             if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found. ");
                return;
            }
            foreach (var task in tasks)
            {
                 Console.WriteLine($"Full_Name, { task.FullName}");
              Console.WriteLine($"Student_age, {task.StudentAge}");
              Console.WriteLine($"Student_course, {task.StudentCourse}");
              Console.WriteLine($"Student_Email, {task.StudentEmail}");
              Console.WriteLine($"Student_id, {task.StudentId}");
            }
        }
        static void UpdateExistingStudent()
        {
             Console.Clear();
            Console.WriteLine("UPDATE TASK\n");
              Console.Write("Enter Task ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int studentid))
            {
                Console.WriteLine("Invalid Task ID!");
                WaitForUser();
                return;
            }

            var existingStudent= taskRepository.GetStudentById(studentid);
            if (existingStudent == null)
            {
                Console.WriteLine("Task not found!");
                WaitForUser();
                return;
            }
              Console.WriteLine($"Current Full Name: {existingStudent.FullName}");
            Console.Write("New Full Name (press enter to keep current): ");
             var newFullname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newFullname))
            {
                existingStudent.FullName = newFullname;
            }

               Console.WriteLine($"Current Student Email: {existingStudent.StudentEmail}");
            Console.Write("New student Email (press enter to keep current): ");
             var newStudentEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newStudentEmail))
            {
                existingStudent.StudentEmail = newStudentEmail;
            }
               Console.WriteLine($"Current student course: {existingStudent.StudentCourse}");
            Console.Write("New Student course (press enter to keep current): ");
             var newStudentCourse = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newStudentCourse))
            {
                existingStudent.StudentCourse =newStudentCourse ;
            }
               Console.WriteLine($"Current Student age: {existingStudent.StudentAge}");
            Console.Write("New Student age (press enter to keep current): ");
           
            if (int.TryParse( Console.ReadLine(), out int newStudentage))
            {
                existingStudent.StudentAge = newStudentage;
            }
             
         

            var success = taskRepository.UpdateStudent(existingStudent);
            if (success)
            {
                Console.WriteLine("\n Task updated successfully!");
            }
            else
            {
                Console.WriteLine("\n Failed to update task!");
            }

            WaitForUser();

            

        }
         static void DeleteAStudent()
        {
            Console.Clear();
            Console.WriteLine(" DELETE TASK\n");

            Console.Write("Enter Task ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int studentid))
            {
                Console.WriteLine(" Invalid Task ID!");
                WaitForUser();
                return;
            }

            var task = taskRepository.GetStudentById(studentid);
            if (task == null)
            {
                Console.WriteLine(" Task not found!");
                WaitForUser();
                return;
            }
               Console.WriteLine($"You are about to delete: {task.FullName}");
            Console.Write("Are you sure? (y/n): ");
            var confirmation = Console.ReadLine();

            if (confirmation?.ToLower() == "y")
            {
                var success = taskRepository.DeleteStudent(studentid);
                if (success)
                {
                    Console.WriteLine("\n Task deleted successfully!");
                }
                else
                {
                    Console.WriteLine("\n Failed to delete task!");
                }
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }

            WaitForUser();
        }
           static void WaitForUser()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }
}
