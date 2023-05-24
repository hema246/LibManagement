using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace LibraryManagement
{
    public class BookDetails
    {
        SqlConnection con = new SqlConnection("Server=IN-F0979S3; database=LibraryManagement; Integrated Security=true");

        public void AddBookDetails()
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from BookDetails", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            
           
            AnsiConsole.MarkupLine("[Green]Enter Book Name:[/]");
            string bookname = Console.ReadLine();
            AnsiConsole.MarkupLine("[Green]Enter Author:[/]");
            string author = Console.ReadLine();
            AnsiConsole.MarkupLine("[Green]Enter publications[/]:");
            string publications = Console.ReadLine();
            AnsiConsole.MarkupLine("[Green]Enter Quantity:[/]");
            int quantity= Convert.ToInt32(Console.ReadLine());

            var row = ds.Tables[0].NewRow();

            row["BookName"] = bookname;
            row["Author"] = author;
            row["Publications"] = publications;
            row["Quantity"] = quantity;

            ds.Tables[0].Rows.Add(row);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            AnsiConsole.MarkupLine("[Yellow]Book details Added[/]");
        }
        public void EditBookDetails()
        {
            try
            {
                AnsiConsole.MarkupLine("[Green]Enter book id[/]");
                int bookid = Convert.ToInt32(Console.ReadLine());
                SqlDataAdapter adp = new SqlDataAdapter($"Select * from BookDetails where BookId={bookid}", con);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                AnsiConsole.MarkupLine("[Green]Enter book name to edit:[/]");
                string bookname = Console.ReadLine();
                AnsiConsole.MarkupLine("[Green]Enter author to edit:[/]");
                string author = Console.ReadLine();
                AnsiConsole.MarkupLine("[Green]Enter publications:[/]");
                string publications = Console.ReadLine();

                var row = ds.Tables[0].Rows[0];
                row["BookName"] = bookname;
                row["Author"] = author;
                row["Publications"] = publications;


                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                AnsiConsole.MarkupLine("[Yellow] BookDetails  Updated[/]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void DeleteBookDetails()
        {
            
            SqlDataAdapter adp = new SqlDataAdapter("Select * from BookDetails", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            AnsiConsole.MarkupLine("[Green]Enter book id to delete:[/]");
            int bookid = Convert.ToInt32(Console.ReadLine());
            try
            {
                ds.Tables[0].Rows[0].Delete();

                SqlCommandBuilder builder = new SqlCommandBuilder(adp);
                adp.Update(ds);
                AnsiConsole.MarkupLine("[Yellow] Book Deleted[/]");
            }
            catch(Exception)
            {
                AnsiConsole.MarkupLine("[Red]Book is issued to student ,So can't delete[/]");
            }
          
        }
    }
}