using System;
using System.Collections.Generic;

namespace VSTT.EFDomain
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public string Isbn { get; set; }
        public DateTime PublishDate { get; set; }

        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get { return _createdDate; }
        }
    }
}