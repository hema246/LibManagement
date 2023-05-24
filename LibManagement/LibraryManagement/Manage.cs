using Microsoft.VisualBasic;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Manage
    {
        SqlConnection con = new SqlConnection("Server=IN-F0979S3; database=LibraryManagement; Integrated Security=true");

        public void IssueBook()
        {
            int bookid = 0;
            try
            {
                AnsiConsole.MarkupLine("[Green]Enter student Roll no[/]");
                int studentrollno = Convert.ToInt32(Console.ReadLine());
                AnsiConsole.MarkupLine("[Green]Enter Book Id[/]");
                bookid = Convert.ToInt32(Console.ReadLine());
                DateTime issuedate = DateTime.Now;
                SqlDataAdapter adp = new SqlDataAdapter($"Select * from IssueBook", con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                var row = ds.Tables[0].NewRow();
                row["BookId"] = bookid;
                row["StudentRollno"] = studentrollno;
                row["IssueDate"] = issuedate;

                ds.Tables[0].Rows.Add(row);
                SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
                adp.Update(ds);
            }catch (Exception )
            {
                AnsiConsole.MarkupLine("[Red]Only one book can issue to one student[/]");
                return;
            }
                //retreiving the row for updating quantity
                SqlDataAdapter adp2 = new SqlDataAdapter($"Select * from BookDetails where BookId={bookid}", con);
                DataSet ds2 = new DataSet();
                adp2.Fill(ds2);
                //retreiving  quantity and decrementing
                int quantity = (int)ds2.Tables[0].Rows[0]["Quantity"];
                ds2.Tables[0].Rows[0]["Quantity"] = quantity - 1;
                SqlCommandBuilder cmd1 = new SqlCommandBuilder(adp2);
                adp2.Update(ds2);
                AnsiConsole.MarkupLine("[Yellow]Book issued to student[/]");
            
        }
        public void ReturnBook()
        {
            try
            {
                AnsiConsole.MarkupLine("[Green]Enter student Rollno[/]");
                int studentrollno = Convert.ToInt32(Console.ReadLine());

                SqlDataAdapter adp = new SqlDataAdapter($"Select * from IssueBook WHERE StudentRollno = {studentrollno}", con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                //retreiving book id for incrementing the quantity before deleting
                int bookid = (int)ds.Tables[0].Rows[0]["BookId"];
                Console.WriteLine(bookid);
                ds.Tables[0].Rows[0].Delete();
                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                AnsiConsole.MarkupLine("[Yellow]Book returned[/]");

                SqlDataAdapter adp1 = new SqlDataAdapter($" Select * from BookDetails where BookId= {bookid}", con);
                DataSet ds1 = new DataSet();
                adp1.Fill(ds1);
                //getting current quantity value 
                int quantity = (int)ds1.Tables[0].Rows[0]["Quantity"];
                ds1.Tables[0].Rows[0]["Quantity"] = quantity + 1;

                SqlCommandBuilder build = new SqlCommandBuilder(adp1);
                adp1.Update(ds1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SearchBook()
        {
            try
            {   //searching book with author name
                AnsiConsole.MarkupLine("[Green]Enter the Author to search[/]");
                string author = Console.ReadLine();
                SqlDataAdapter adp = new SqlDataAdapter($"Select * from BookDetails WHERE Author='{author}'", con);
                DataSet ds = new DataSet();
                adp.Fill(ds, "Manage");
                for (int i = 0; i < ds.Tables["Manage"].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables["Manage"].Columns.Count; j++)
                    {
                        Console.Write($"{ds.Tables["Manage"].Rows[i][j]} || ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                AnsiConsole.MarkupLine("[Red]Enter correct Author name[/]");
            }
        }
        public void SearchStudent()
        {
            try
            {   
                //searching students with roll no
                AnsiConsole.MarkupLine("[Green]Enter Student Rollno to search[/]");
                int studentrollno = Convert.ToInt32(Console.ReadLine());

                SqlDataAdapter adp = new SqlDataAdapter($"Select * from StudentDetails WHERE StudentRollno ={studentrollno}", con);
                DataSet ds = new DataSet();
                adp.Fill(ds, "Manage");

                for (int i = 0; i < ds.Tables["Manage"].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables["Manage"].Columns.Count; j++)
                    {
                        Console.Write($"{ds.Tables["Manage"].Rows[i][j]} || ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception)
            {
                AnsiConsole.MarkupLine("[Red]Enter correct roll no[/]");
            }
        }
        public void StudentsHavingBooks()
        {              
             //count available no of students having books
             SqlDataAdapter adp = new SqlDataAdapter($"Select * FROM IssueBook", con);
             DataSet ds = new DataSet();
             adp.Fill(ds);
            //returns the studentdetails 

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        Console.Write($"{ds.Tables[0].Rows[i][j]} || ");
                    }
                    Console.WriteLine();
                }
                
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
        }
    }
}