using System;
using APProjectBackend.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace APProjectBackend.Model.Repositories;

public class RatingRepository : BaseRepository
{
    public RatingRepository(IConfiguration configuration) : base(configuration) { }
    public Rating GetRatingById(int rating_id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from ratings where rating_id = @rating_id";
            cmd.Parameters.Add("@rating_id", NpgsqlDbType.Integer).Value = rating_id;
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                if (data.Read()) //every time loop runs it reads next like from fetched rows
                {
                    return new Rating(Convert.ToInt32(data["rating_id"]))
                    {
                        Rating_id = int.Parse(data["rating_id"].ToString()),                                  // see if int.TryParse necessary
                        rating = int.Parse(data["rating"].ToString())
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
    public List<Rating> GetRatings()
    {
        NpgsqlConnection dbConn = null;
        var ratings = new List<Rating>();
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from ratings";
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read()) //every time loop runs it reads next like from fetched rows
                {
                    Rating r = new Rating(Convert.ToInt32(data["rating_id"]))
                    {
                        Rating_id = int.Parse(data["rating_id"].ToString()),                                  // see if int.TryParse necessary
                        rating = int.Parse(data["rating"].ToString())
                    };
                    ratings.Add(r);
                }
            }
            return ratings;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    //add a new rating
    public bool InsertRating(Rating r)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
insert into ratings
(Rating_id, rating)
values
(@Rating_id, @rating)
";
            //adding parameters in a better way
            cmd.Parameters.AddWithValue("@rating_id", NpgsqlDbType.Text, r.Rating_id);
            cmd.Parameters.AddWithValue("@rating", NpgsqlDbType.Text, r.rating);
            //will return true if all goes well
            bool result = InsertData(dbConn, cmd);
            return result;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    public bool UpdateRating(Rating r)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
update ratings set
rating=@rating
where
Rating_id=@Rating_id";
        cmd.Parameters.AddWithValue("@rating_id", NpgsqlDbType.Text, r.Rating_id);
            cmd.Parameters.AddWithValue("@rating", NpgsqlDbType.Text, r.rating);
        bool result = UpdateData(dbConn, cmd);
        return result;
    }
    public bool DeleteRating(int id)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
delete from ratings
where Rating_id = @rating_id
";
        //adding parameters in a better way
        cmd.Parameters.AddWithValue("@rating_id", NpgsqlDbType.Integer, id);
        //will return true if all goes well
        bool result = DeleteData(dbConn, cmd);
        return result;
    }
}