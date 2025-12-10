namespace APProjectBackend.Model.Entities;
public class Author
{
    public Author(int author_id) { Author_id = author_id; }
    public int Author_id { get; set; }
    public string author_name { get; set; }

}
