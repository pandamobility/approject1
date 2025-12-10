namespace APProjectBackend.Model.Entities;
public class Publisher
{
    public Publisher(int publisher_id) { Publisher_id = publisher_id; }
    public int Publisher_id { get; set; }
    public string publisher_name { get; set; }

}
