using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Spectre.Console;

namespace LibraryManagement
{
    public class StudentDetails:IStudentDetails
    {
        SqlConnection con = new SqlConnection("Server=IN-F0979S3; database=LibraryManagement; Integrated Security=true");

        public int  AddStudentDetails()
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from StudentDetails", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var row = ds.Tables[0].NewRow();

            AnsiConsole.MarkupLine("[Green]Enter Student name:[/]");
            string studentname = Console.ReadLine();
            AnsiConsole.MarkupLine("[Green]Enter Roll no[/]");
            int studentrollno=Convert.ToInt32(Console.ReadLine());
            
            AnsiConsole.MarkupLine("[Green]Enter student email id:[/]");
            string studentemail = Console.ReadLine();
            AnsiConsole.MarkupLine("[Green]Enter student department:[/]");
            string studentdept = Console.ReadLine();


            row["StudentName"] = studentname;
            row["StudentRollno"] = studentrollno;
            row["StudentEmail"] = studentemail;
            row["StudentDept"] = studentdept;
           
            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);

            int result=adp.Update(ds);
            AnsiConsole.MarkupLine("[Yellow]Student Details Added[/]");
            return result;
            
        }
        public int EditStudentDetails()
        {
            AnsiConsole.MarkupLine("[Green]Enter student Roll no to edit[/]");
            int studentrollno = Convert.ToInt32(Console.ReadLine());

            SqlDataAdapter adp = new SqlDataAdapter($"Select * from StudentDetails where StudentRollno={studentrollno}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            AnsiConsole.MarkupLine("[Green]Enter student name to edit:[/]");
            string studentname = Console.ReadLine();
           
            AnsiConsole.MarkupLine("[Green]Enter student email to update:[/]");
            string studentemail = Console.ReadLine();
            AnsiConsole.MarkupLine("[Green]Enter student dept:[/]");
            string studentdept = Console.ReadLine();


            var row = ds.Tables[0].Rows[0];
            row["StudentName"] = studentname;     
            row["StudentEmail"] = studentemail;
            row["StudentDept"] = studentdept;
            
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            int result=adp.Update(ds);
            AnsiConsole.MarkupLine("[Yellow] Student Database Updated[/]");
            return result;

        }
        public int DeleteStudentDetails()
        {          
            SqlDataAdapter adp = new SqlDataAdapter("Select * from StudentDetails", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            AnsiConsole.MarkupLine("[Green]Enter student roll no to delete:[/]");
            int studentid = Convert.ToInt32(Console.ReadLine());

            ds.Tables[0].Rows[0].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            int result=adp.Update(ds);
            AnsiConsole.MarkupLine("[Yellow]Student details deleted[/]");
            return result;
        }
    }
}
