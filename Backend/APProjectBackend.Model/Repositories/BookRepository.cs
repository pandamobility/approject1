using System;
using APProjectBackend.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace APProjectBackend.Model.Repositories;

public class BookRepository : BaseRepository
{
    public BookRepository (IConfiguration configuration) : base(configuration)
    { }
    public Book GetBookById(int book_id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from book where book_id = @book_id";
            cmd.Parameters.Add("@book_id", NpgsqlDbType.Integer).Value = book_id;
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new Book(Convert.ToInt32(data["book_id"]))
                    {
                        Book_id = int.Parse(data["book_id"].ToString()),                                  // see if int.TryParse necessary
                        book_title = data["book_title"].ToString(),
                        author_id = int.Parse(data["author_id"].ToString()),
                        genre_id = int.Parse(data["genre_id"].ToString()),
                        publisher_id = int.Parse(data["publisher_id"].ToString()),
                        review = data["review"].ToString(),
                        page_count = int.Parse(data["page_count"].ToString()),
                        year_published = int.Parse(data["year_published"].ToString()),
                    };
                }
            }
            return null;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    public List<Book> GetBook()
    {
        NpgsqlConnection dbConn = null;
        var books = new List<Book>();
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from book";
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    Book b = new Book(Convert.ToInt32(data["book_id"]))
                    {
                        Book_id = int.Parse(data["book_id"].ToString()),                            // see if int.Parse necessary
                        book_title = data["book_title"].ToString(),
                    };
                    books.Add(b);
                }
            }
            return books;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    //add a new author
    public bool InsertBook(Book b)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
insert into book
(book_title)
values
(@book_title)
";
            //adding parameters in a better way                                 ----------------------                        ! ! !
            cmd.Parameters.AddWithValue("@book_title", NpgsqlDbType.Text,
            b.book_title);
            //will return true if all goes well
            bool result = InsertData(dbConn, cmd);
            return result;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    public bool UpdateBook(Book b)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
update book set
book_title=@book_title,
where
book_id = @book_id";
        cmd.Parameters.AddWithValue("@book_title", NpgsqlDbType.Text, b.book_title);
        cmd.Parameters.AddWithValue("@book_id", NpgsqlDbType.Integer, b.Book_id);
        bool result = UpdateData(dbConn, cmd);
        return result;
    }
    public bool DeleteBook(int book_id)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
delete from book
where book_id = @book_id
";
        //adding parameters in a better way
        cmd.Parameters.AddWithValue("@book_id", NpgsqlDbType.Integer, book_id);
        //will return true if all goes well
        bool result = DeleteData(dbConn, cmd);
        return result;
    }
}
