using System;
using System.Collections.Generic;
using System.Data.Entity;
using DemoApp.Data.Entities;

namespace DemoApp.Data
{
    public class DemoAppDataContextInitializer : DropCreateDatabaseIfModelChanges<DemoAppDataContext>
    {
        protected override void Seed(DemoAppDataContext context)
        {
            var persons = new List<Person>
            {
                new Person { Name = "John Smith", Description = "Cool man", Photo="", Gender = 0, BirthDate = DateTime.Now.AddYears(-30), FirstName = "John", LastName = "Smith" },
                new Person { Name = "Tom Smith", Description = "Cool man", Photo="", Gender = 0, BirthDate = DateTime.Now.AddYears(-40), FirstName = "Tom", LastName = "Smith" },
                new Person { Name = "Dan Smith", Description = "Cool man", Photo="",  Gender = 0, BirthDate = DateTime.Now.AddYears(-35), FirstName = "Dan", LastName = "Smith" },
            };

            var books = new List<Book>
            {
                new Book { Name = "Book 1", Description = "Sample book 1", Photo="", Published = DateTime.Now, Copyright = "Copyright 2012", Author = persons[0] },
                new Book { Name = "Book 2", Description = "Sample book 2", Photo="", Published = DateTime.Now, Copyright = "Copyright 2012", Author = persons[1] },
                new Book { Name = "Book 3", Description = "Sample book 3", Photo="", Published = DateTime.Now, Copyright = "Copyright 2012", Author = persons[2] },
                new Book { Name = "Book 4", Description = "Sample book 4", Photo="", Published = DateTime.Now, Copyright = "Copyright 2012", Author = persons[1] }
            };

            persons.ForEach(x => context.Persons.Add(x));
            books.ForEach(x => context.Books.Add(x));

            context.SaveChanges();
        }
    }
}
