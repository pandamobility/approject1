using System;
using APProjectBackend.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace APProjectBackend.Model.Repositories;

public class PublisherRepository : BaseRepository
{
    public PublisherRepository(IConfiguration configuration) : base(configuration)
    { }
    public Publisher GetPublisherById(int publisher_id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from publisher where publisher_id = @publisher_id";
            cmd.Parameters.Add("@publisher_id", NpgsqlDbType.Integer).Value = publisher_id;
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new Publisher(Convert.ToInt32(data["publisher_id"]))
                    {
                        Publisher_id = int.Parse(data["publisher_id"].ToString()),                                  // see if int.TryParse necessary
                        publisher_name = data["publisher_name"].ToString(),
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
    public List<Publisher> GetPublishers()
    {
        NpgsqlConnection dbConn = null;
        var publishers = new List<Publisher>();
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL commandS
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from publisher";
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    Publisher a = new Publisher(Convert.ToInt32(data["publisher_id"]))
                    {
                        Publisher_id = int.Parse(data["publisher_id"].ToString()),                            // see if int.Parse necessary
                        publisher_name = data["publisher_name"].ToString(),
                    };
                    publishers.Add(a);
                }
            }
            return publishers;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    //add a new publisher
    public bool InsertPublisher(Publisher a)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
insert into publisher
(publisher_name)
values
(@publisher_name)
";
            //adding parameters in a better way                                 ----------------------                        ! ! !
            cmd.Parameters.AddWithValue("@publisher_name", NpgsqlDbType.Text,
            a.publisher_name);
            //will return true if all goes well
            bool result = InsertData(dbConn, cmd);
            return result;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    public bool UpdatePublisher(Publisher a)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
update publisher set
publisher_name=@publisher_name,
where
publisher_id = @publisher_id";
        cmd.Parameters.AddWithValue("@publisher_name", NpgsqlDbType.Text, a.publisher_name);
        cmd.Parameters.AddWithValue("@publisher_id", NpgsqlDbType.Integer, a.Publisher_id);
        bool result = UpdateData(dbConn, cmd);
        return result;
    }
    public bool DeletePublisher(int publisher_id)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
delete from publisher
where publisher_id = @publisher_id
";
        //adding parameters in a better way
        cmd.Parameters.AddWithValue("@publisher_id", NpgsqlDbType.Integer, publisher_id);
        //will return true if all goes well
        bool result = DeleteData(dbConn, cmd);
        return result;
    }
}