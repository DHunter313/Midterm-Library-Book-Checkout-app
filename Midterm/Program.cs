using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksIO;
using System.IO;


namespace BooksIO
{
    class Program
    {
        static void Main(string[] args)
        {
            loadBooks();
            List<Book> receiptList = new List<Book>();


            string dueDate = Convert.ToString(DateTime.Now.AddDays(14).ToShortDateString());
            Console.WriteLine("Welcome to Titan Library");
            string cont = "";
            do

            {
                BookProcess();
                Console.Clear();

                Console.WriteLine("Would you like to see all of our selection? y/n?");
                string cBooks = Console.ReadLine();
                Console.Clear();
                if (cBooks == "y" || cBooks == "Y")
                {
                    for (int i = 0; i < default_BookCatalog.Count; i++)
                    {
                        Book p = default_BookCatalog[i];
                        string outputString = getFormattedStringWithSpaces(p.v5bookCode, 10)
                            + getFormattedStringWithSpaces(p.v1bookTitle, 30)
                            + getFormattedStringWithSpaces(p.v2bookAuthor, 20);
                        Console.WriteLine(outputString);

                    }
                }
                else { Console.WriteLine("Please take a look at our catalog then. Here you can choose to see our books in a certain order.\n\n"); }
                Console.WriteLine("\nWhat would you like to read today? \n\nType 1= title,2= author,or 3= keyword");
                int displayRead = Convert.ToInt32(Console.ReadLine());
                string cat = "";
                int number = 0;

                Book n = default_BookCatalog[number];


                if (displayRead == 1)
                {
                    Console.WriteLine("Book Code Book Title                    Book Author");

                }

                else if (displayRead == 2)
                {

                    Console.WriteLine("Book Code Book Author                    Book Title");

                }
                else if (displayRead == 3)
                {

                    Console.WriteLine("Book Code Book Keyword   Book Title                    Book Author");

                }

                for (int i = 0; i < default_BookCatalog.Count; i++)
                {
                    n = default_BookCatalog[i];

                    if (displayRead == 1)
                    {
                        cat = getFormattedStringWithSpaces(n.v5bookCode, 10)
                          + getFormattedStringWithSpaces(n.v1bookTitle, 30)
                          + getFormattedStringWithSpaces(n.v2bookAuthor, 20);

                    }

                    else if (displayRead == 2)
                    {

                        cat = getFormattedStringWithSpaces(n.v5bookCode, 10)
                          + getFormattedStringWithSpaces(n.v2bookAuthor, 30)
                          + getFormattedStringWithSpaces(n.v1bookTitle, 20);


                    }
                    else if (displayRead == 3)
                    {
                        cat = getFormattedStringWithSpaces(n.v5bookCode, 10)
                          + getFormattedStringWithSpaces(n.v3bookKeyword, 15)
                          + getFormattedStringWithSpaces(n.v1bookTitle, 30)
                          + getFormattedStringWithSpaces(n.v2bookAuthor, 20);
                    }
                    Console.WriteLine(cat);
                }
                string userAnotherBook;
                do
                {
                    Console.WriteLine("Choose a book by the 3-digit Book Code");
                    string userBookCode = (Console.ReadLine());
                    n = GetBook(userBookCode);

                    if (n.v4bookStatus == "check")
                    {

                        Console.WriteLine("\nSorry, that book is unavailable. It is due back " + n.v6dueDate);
                    }
                    else if (n.v4bookStatus != "check")
                    {
                        Console.WriteLine($"\n{n.v1bookTitle} is Available. ");
                        receiptList.Add(n);
                        n.v4bookStatus = "check";
                        n.v6dueDate = dueDate;
                        saveBooks();

                    }
                    else
                    {
                        Console.WriteLine("\n\nPlease enter the correct three digit Book Code");
                        userAnotherBook = Console.ReadLine();
                    }
                    Console.WriteLine("\nDo you want to add another? y/n");
                    userAnotherBook = Console.ReadLine();

                } while (userAnotherBook == "y");

                Console.Clear();

                for (int i = 0; i < receiptList.Count; i++)
                {
                    Book BookCheckOut = receiptList[i];
                    Console.WriteLine("You've checked out \n" + BookCheckOut.v1bookTitle + "\t" + BookCheckOut.v2bookAuthor + "\t" + BookCheckOut.v5bookCode);


                }
                DateTime localDate = DateTime.Now.AddDays(14);
                Console.WriteLine("You're book is due " + dueDate);




                Console.WriteLine("Do you want to continue? y/n?");
                cont = Console.ReadLine().ToLower().Trim();

            } while (cont == "y");
            Console.WriteLine("Thank you for your Inquiry");

            Console.ReadKey();


        }

        public static string getFormattedStringWithSpaces(string aString, int width)
        {
            string returnString = aString;
            int length = aString.Length;
            while (length < width)
            {
                returnString += " ";
                length = returnString.Length;
            }
            return returnString;
        }

        public static Book GetBook(string bookCode)
        {
            Book testBook = new Book("not valid selection", "not valid selection", "not valid selection", "check", " ", " ");
            foreach (var item in default_BookCatalog)
            {
                if (item.v5bookCode == bookCode)
                {
                    testBook = item;

                }
            }
            return testBook;
        }

        public static void loadBooks()
        {
            default_BookCatalog.Clear();
            var reader = new StreamReader(File.OpenRead(@"C:\Sample.txt"));
            List<Book> listBooks = new List<Book>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                if (values.Length == 6)
                {
                    Book book = new Book(values[0], values[1], values[2], values[3], values[4], values[5]);

                    default_BookCatalog.Add(book);
                }

            }
            reader.Close();

        }
        public static void saveBooks()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(@"C:\Sample.txt");
                foreach (var book in default_BookCatalog)
                {
                    string line = book.v1bookTitle;
                    line += "," + book.v2bookAuthor;
                    line += "," + book.v3bookKeyword;
                    line += "," + book.v4bookStatus;
                    line += "," + book.v5bookCode;
                    line += "," + book.v6dueDate;
                    writer.WriteLine(line);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }

        }


        public static void BookProcess()
        {
            string AnotherReturn;

            Console.WriteLine("Are you: 1. Returning the book? or 2. Checking out a book?\n  Choose 1 or 2");
            int userChoice = int.Parse(Console.ReadLine());

            if (userChoice == 1)
            {

                do
                {
                    Console.WriteLine("\n\nPlease enter your 3 digit book code");
                    string returnCode = Console.ReadLine();


                    Book returnedBook = GetBook(returnCode);
                    if (returnCode == returnedBook.v5bookCode && returnedBook.v4bookStatus == "check")
                    {
                        Console.WriteLine("Your book " + returnedBook.v1bookTitle + " " + returnedBook.v2bookAuthor + " " + returnedBook.v5bookCode + " is checked in.");
                        returnedBook.v4bookStatus = " ";
                        returnedBook.v6dueDate = " ";
                        saveBooks();

                        Console.WriteLine("Would you like  to return another book?  (y/n)");
                        AnotherReturn = Console.ReadLine();

                    }

                    else
                    {
                        Console.WriteLine("Book is not checked out");
                        Console.WriteLine("Please check the number on your book");
                        Console.WriteLine("Do you still want to return a book? (y/n)");
                        AnotherReturn = Console.ReadLine();
                    }
                }
                while (AnotherReturn == "Y" || AnotherReturn == "y");



            }

        }

        public static List<Book> default_BookCatalog = new List<Book>();

    }

}
