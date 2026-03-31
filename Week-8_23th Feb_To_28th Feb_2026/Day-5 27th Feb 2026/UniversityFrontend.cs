using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace UniversityFrontend
{
    internal class Program
    {
        static string connectionString =
            "Data Source=DESKTOP-CT4MMTD\\SQLEXPRESS01;Initial Catalog=UniversityDB;TrustServerCertificate=True;Integrated Security=True;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n---- University Management ----");
                Console.WriteLine("1. Insert Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. View Student By Id");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.Write("Select Option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        InsertStudent();
                        break;
                    case 2:
                        GetAllStudents();
                        break;
                    case 3:
                        GetStudentById();
                        break;
                    case 4:
                        UpdateStudent();
                        break;
                    case 5:
                        DeleteStudent();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }

        static void InsertStudent()
        {
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            ShowDepartments();

            Console.Write("Enter DeptId from above list: ");
            int deptId = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DeptId", deptId);

                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Student Inserted Successfully");
            }
        }

        static void GetAllStudents()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllStudents", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(
                        reader["StudentId"] + " | " +
                        reader["FirstName"] + " " +
                        reader["LastName"] + " | " +
                        reader["Email"] + " | " +
                        reader["DeptName"]);
                }
            }
        }

        static void GetStudentById()
        {
            Console.Write("Enter Student Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetStudentById", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentId", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine(
                        reader["StudentId"] + " | " +
                        reader["FirstName"] + " " +
                        reader["LastName"] + " | " +
                        reader["Email"] + " | DeptId: " +
                        reader["DeptId"]);
                }
                else
                {
                    Console.WriteLine("Student Not Found");
                }
            }
        }

        static void UpdateStudent()
        {
            Console.Write("Student Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("DeptId: ");
            int deptId = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentId", id);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DeptId", deptId);

                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Student Updated Successfully");
            }
        }

        static void DeleteStudent()
        {
            Console.Write("Enter Student Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Student Deleted Successfully");
            }
        }

        static void ShowDepartments()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT DeptId, DeptName FROM Departments", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine("\nAvailable Departments:");
                while (reader.Read())
                {
                    Console.WriteLine(reader["DeptId"] + " - " + reader["DeptName"]);
                }
            }
        }
    }
}