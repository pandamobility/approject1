using System;
using APProjectBackend.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace APProjectBackend.Model.Repositories;

public class AuthorRepository : BaseRepository
{
    public AuthorRepository(IConfiguration configuration) : base(configuration)
    { }
    public Author GetAuthorById(int author_id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from author where author_id = @author_id";
            cmd.Parameters.Add("@author_id", NpgsqlDbType.Integer).Value = author_id;
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new Author(Convert.ToInt32(data["author_id"]))
                    {
                        Author_id = int.Parse(data["author_id"].ToString()),                                  // see if int.TryParse necessary
                        author_name = data["author_name"].ToString(),
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
    public List<Author> GetAuthors()
    {
        NpgsqlConnection dbConn = null;
        var authors = new List<Author>();
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from author";
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    Author a = new Author(Convert.ToInt32(data["author_id"]))
                    {
                        Author_id = int.Parse(data["author_id"].ToString()),                            // see if int.Parse necessary
                        author_name = data["author_name"].ToString(),
                    };
                    authors.Add(a);
                }
            }
            return authors;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    //add a new author
    public bool InsertAuthor(Author a)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
insert into author
(author_name
(@author_name)
";
            //adding parameters in a better way                                 ----------------------                        ! ! !
            cmd.Parameters.AddWithValue("@author_name", NpgsqlDbType.Text,
            a.author_name);
            //will return true if all goes well
            bool result = InsertData(dbConn, cmd);
            return result;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    public bool UpdateAuthor(Author a)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
update author set
author_name=@author_name,
where
author_id = @author_id";
        cmd.Parameters.AddWithValue("@author_name", NpgsqlDbType.Text, a.author_name);
        cmd.Parameters.AddWithValue("@author_id", NpgsqlDbType.Integer, a.Author_id);
        bool result = UpdateData(dbConn, cmd);
        return result;
    }
    public bool DeleteAuthor(int author_id)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
delete from author
where author_id = @author_id
";
        //adding parameters in a better way
        cmd.Parameters.AddWithValue("@author_id", NpgsqlDbType.Integer, author_id);
        //will return true if all goes well
        bool result = DeleteData(dbConn, cmd);
        return result;
    }
}
