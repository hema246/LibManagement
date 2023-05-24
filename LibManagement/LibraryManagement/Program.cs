using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.ComponentModel.Design;
using LibraryManagement;

namespace LibraryManagement
{

    internal class Program
    {
        
        static void Main(string[] args)
        {   
            //created instance for classes
            BookDetails book = new BookDetails();
            StudentDetails student = new StudentDetails();
            Manage manage = new Manage();
            //login
            AnsiConsole.Write(new FigletText("LibManagement").Centered().Color(Color.BlueViolet));
            AnsiConsole.MarkupLine("*********[Blue] LOGIN [/] *********");
            AnsiConsole.MarkupLine("[Green]Enter UserId[/]");
            int userid = Convert.ToInt32(Console.ReadLine());
            AnsiConsole.MarkupLine("[Green]Enter User Password[/]");
            string userpw = Console.ReadLine();
            SqlConnection con = new SqlConnection("Server=IN-F0979S3; database=LibraryManagement; Integrated Security=true");
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from LoginDetails where UserId='{userid}'AND UserPW='{userpw}'", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            int Count = ds.Tables[0].Rows.Count;
            if (Count > 0)
            {
                AnsiConsole.MarkupLine("[Yellow]Successfully Logged In[/]");
                SqlCommandBuilder cmd = new SqlCommandBuilder(adp);
                adp.Update(ds);

                while (true)
                {        
                    var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[Red] Select your option [/]")
                    .AddChoices(new[] {
                    "Add Book Details", "Edit Book Details", "Delete Book Details",
                    "Add Student Details","Edit Student Details","Delete Student Details","Issue Book","Return Book",
                    "Search Book","Search Student","Students Having Books"
                                }));
                    AnsiConsole.MarkupLine($"[Violet]You selected {choice}[/]");
                    switch (choice)
                    {
                        case "Add Book Details":
                            {
                                book.AddBookDetails();
                                break;
                            }
                        case "Edit Book Details":
                            {
                                book.EditBookDetails();
                                break;
                            }
                        case "Delete Book Details":
                            {
                                book.DeleteBookDetails();
                                break;
                            }
                        case "Add Student Details":
                            {
                                student.AddStudentDetails();
                                break;
                            }
                        case "Edit Student Details":
                            {
                                student.EditStudentDetails();
                                break;
                            }
                        case "Delete Student Details":
                            {
                                student.DeleteStudentDetails();
                                break;
                            }
                        case "Issue Book":
                            {
                                manage.IssueBook();
                                break;
                            }
                        case "Return Book":
                            {
                                manage.ReturnBook();
                                break;
                            }
                        case "Search Book":
                            {
                                manage.SearchBook();
                                break;
                            }

                        case "Search Student":
                            {
                                manage.SearchStudent();
                                break;
                            }
                        case "Students Having Books":
                            {
                                manage.StudentsHavingBooks();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Enter a valid option");
                                break;
                            }
                    }
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[Red]Login Failed[/]");
            }
        }
       

    }
    
}