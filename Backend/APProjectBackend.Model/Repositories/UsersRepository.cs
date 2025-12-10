using System;
using APProjectBackend.Model.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
namespace APProjectBackend.Model.Repositories;

public class UsersRepository : BaseRepository
{
    public UsersRepository(IConfiguration configuration) : base(configuration)
    { }
    public Users GetUsersById(int user_id)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from users where user_id = @user_id";
            cmd.Parameters.Add("@user_id", NpgsqlDbType.Integer).Value = user_id;
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                if (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    return new Users(Convert.ToInt32(data["user_id"]))
                    {
                        username = data["username"].ToString(),
                        user_firstname = data["user_firstname"].ToString(),
                        email = data["email"].ToString(),
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
    public List<Users> GetUsers()
    {
        NpgsqlConnection dbConn = null;
        var users = new List<Users>();
        try
        {
            //create a new connection for database
            dbConn = new NpgsqlConnection(ConnectionString);
            //creating an SQL command
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from users";
            //call the base method to get data
            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read()) //every time loop runs it reads next like from fetched rows
            {
                    Users u = new Users(Convert.ToInt32(data["user_id"]))
                    {
                        username = data["username"].ToString(),
                        user_firstname = data["user_firstname"].ToString(),
                        email = data["email"].ToString(),
                    };
                    users.Add(u);
                }
            }
            return users;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    //add a new author
    public bool InsertUsers(Users u)
    {
        NpgsqlConnection dbConn = null;
        try
        {
            dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
insert into users
(username, user_firstname, email)
values
(@username, @user_firstname, @email)
";
            //adding parameters in a better way                                 ----------------------                  do I need to add this for all parameters?
            cmd.Parameters.AddWithValue("@username", NpgsqlDbType.Text,
            u.username);
            cmd.Parameters.AddWithValue("@user_firstname", NpgsqlDbType.Text,
            u.user_firstname);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text,
            u.email);
            //will return true if all goes well
            bool result = InsertData(dbConn, cmd);
            return result;
        }
        finally
        {
            dbConn?.Close();
        }
    }
    public bool UpdateUsers(Users u)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
update users set
username=@username,
where
user_id = @user_id";
        cmd.Parameters.AddWithValue("@username", NpgsqlDbType.Text, u.username);
        cmd.Parameters.AddWithValue("@user_id", NpgsqlDbType.Integer, u.User_id);
        cmd.Parameters.AddWithValue("@user_firstname", NpgsqlDbType.Integer, u.user_firstname);
        cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Integer, u.email);
        bool result = UpdateData(dbConn, cmd);
        return result;
    }
    public bool DeleteUsers(int user_id)
    {
        var dbConn = new NpgsqlConnection(ConnectionString);
        var cmd = dbConn.CreateCommand();
        cmd.CommandText = @"
delete from users
where user_id = @user_id
";
        //adding parameters in a better way
        cmd.Parameters.AddWithValue("@user_id", NpgsqlDbType.Integer, user_id);
        //will return true if all goes well
        bool result = DeleteData(dbConn, cmd);
        return result;
    }
}
