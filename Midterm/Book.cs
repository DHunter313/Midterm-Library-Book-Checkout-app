
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksIO
{
    public class Book
    {
        public string v4bookStatus;
        public string v1bookTitle;
        public string v2bookAuthor;
        public string v3bookKeyword;
        public string v5bookCode;
        public string v6dueDate;

        public Book(string v1, string v2, string v3, string v4, string v5, string v6)
        {
            this.v1bookTitle = v1;
            this.v2bookAuthor = v2;
            this.v3bookKeyword = v3;
            this.v4bookStatus = v4;
            this.v5bookCode = v5;
            this.v6dueDate = v6;
        }

        public bool bookCategory(string keyword)
        {
            return this.v3bookKeyword == keyword;
        }


    }
}