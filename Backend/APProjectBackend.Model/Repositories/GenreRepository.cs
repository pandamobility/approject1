using System;
using APProjectBackend.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace APProjectBackend.Model.Repositories;

public class GenreRepository : BaseRepository
{
    public GenreRepository(IConfiguration configuration) : base(configuration)
    { }
    public Genre GetGenreById(int genre_id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from genre where genre_id = @genre_id";
            cmd.Parameters.Add("@genre_id", NpgsqlDbType.Integer).Value = genre_id;
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new Genre(Convert.ToInt32(data["genre_id"]))
                    {
                        Genre_id = int.Parse(data["genre_id"].ToString()),                                  // see if int.TryParse necessary
                        genre_name = data["genre_name"].ToString(),
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
    public List<Genre> Genres()
    {
        NpgsqlConnection dbConn = null;
        var genres = new List<Genre>();
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from genre";
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    Genre a = new Genre(Convert.ToInt32(data["genre_id"]))
                    {
                        Genre_id = int.Parse(data["genre_id"].ToString()),                            // see if int.Parse necessary
                        genre_name = data["genre_name"].ToString(),
                    };
                    genres.Add(a);
                }
            }
            return genres;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    //add a new genre
    public bool InsertGenre(Genre a)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
insert into genre
(genre_name)
values
(@genre_name)
";
            //adding parameters in a better way                                 ----------------------                        ! ! !
            cmd.Parameters.AddWithValue("@genre_name", NpgsqlDbType.Text,
            a.genre_name);
            //will return true if all goes well
            bool result = InsertData(dbConn, cmd);
            return result;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    public bool UpdateGenre(Genre a)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
update genre set
genre_name=@genre_name,
where
genre_id = @genre_id";
        cmd.Parameters.AddWithValue("@genre_name", NpgsqlDbType.Text, a.genre_name);
        cmd.Parameters.AddWithValue("@genre_id", NpgsqlDbType.Integer, a.Genre_id);
        bool result = UpdateData(dbConn, cmd);
        return result;
    }
    public bool DeleteGenre(int genre_id)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
delete from genre
where genre_id = @genre_id
";
        //adding parameters in a better way
        cmd.Parameters.AddWithValue("@genre_id", NpgsqlDbType.Integer, genre_id);
        //will return true if all goes well
        bool result = DeleteData(dbConn, cmd);
        return result;
    }

    public object GetGenre()
    {
        throw new NotImplementedException();
    }
}
