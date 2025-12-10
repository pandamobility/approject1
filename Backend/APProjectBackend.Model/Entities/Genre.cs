namespace APProjectBackend.Model.Entities;
public class Genre
{
    public Genre(int genre_id) { Genre_id = genre_id; }
    public int Genre_id { get; set; }
    public string genre_name { get; set; }

}
