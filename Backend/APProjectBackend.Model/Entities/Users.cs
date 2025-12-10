namespace APProjectBackend.Model.Entities;
public class Users
{
    public Users (int user_id) { User_id = user_id; }
    public int User_id { get; set; }
    public string username { get; set; }
    public string user_firstname { get; set; }
    public string email { get; set; }

}
