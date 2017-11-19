using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Northwind.mdf;Integrated Security=True;Connect Timeout=30");
            conn.Open();
            string comm = "SELECT * FROM Employees WHERE EmployeeID = 8";
            SqlCommand sqlCommand = new SqlCommand(comm,conn);
            var reader = sqlCommand.ExecuteReader();
            Console.WriteLine("1.   Show all info about the employee with ID 8.");
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader[i]+"\t");
            }
            reader.Close();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("2.	Show the list of first and last names of the employees from London.");

            reader = new SqlCommand("SELECT FirstName, LastName FROM Employees WHERE City = 'London'",conn).ExecuteReader();

            Console.WriteLine("First Name\tLast Name");
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}\t\t{reader[1]}");
            }
            reader.Close();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("11.	Show the list of all cities where the employees are from.");

            reader = new SqlCommand("SELECT City FROM Employees GROUP BY City", conn).ExecuteReader();

            Console.WriteLine("City");
            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
            }
            reader.Close();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("12.	Show first, last names and dates of birth of the employees who celebrate their birthdays this month.");

            var d = DateTime.Now;
            var t = new TimeSpan(-d.Day+1, -d.Hour, -d.Minute, -d.Second);
            var d2 = d + t;
            var d3 = d2.AddMonths(1);

            reader = new SqlCommand($"SELECT FirstName, LastName, BirthDate FROM Employees WHERE MONTH(BirthDate) = {DateTime.Now.Month}", conn).ExecuteReader();

            Console.WriteLine("First Name\tLast Name\tBirthDate");
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}");
            }
            reader.Close();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("22.	*Show the list of french customers’ names who used to order french products (use left join).");

            reader = new SqlCommand($"SELECT CompanyName FROM Customers LEFT JOIN Orders on Customers.CustomerID = Orders.CustomerID AND ShipCountry = 'France' WHERE Country = 'France' AND ShipCountry IS NOT NULL GROUP BY CompanyName", conn).ExecuteReader();

            Console.WriteLine("Company Name");
            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
            }
            reader.Close();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("23.	*Show the list of french customers’ names who used to order french products.");

            reader = new SqlCommand($"SELECT DISTINCT CompanyName FROM Customers, Orders WHERE Customers.CustomerID = Orders.CustomerID AND Country = 'France' AND ShipCountry = 'France'", conn).ExecuteReader();

            Console.WriteLine("Company Name");
            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
            }
            reader.Close();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("33.	*Change the City field in one of your records using the UPDATE statement.");

            var rows = new SqlCommand($"UPDATE Employees SET City = 'Lviv' Where EmployeeID = 8", conn).ExecuteNonQuery();
            Console.WriteLine($"{rows} row affected");

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("35.	*Delete one of your records.");

            var cl = new SqlCommand($"DELETE FROM Region WHERE RegionID = 8", conn).ExecuteNonQuery();
            Console.WriteLine($"{cl} row affected");

            Console.ReadKey();

        }
    }
}
