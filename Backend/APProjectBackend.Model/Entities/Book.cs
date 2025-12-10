namespace APProjectBackend.Model.Entities;
public class Book
{
    public Book(int book_id) { Book_id = book_id; }
    public int Book_id { get; set; }
    public string book_title { get; set; }
    public int author_id { get; set; }
    public int genre_id { get; set; }
    public int publisher_id { get; set; }
    public string review { get; set; }
    public int rating { get; set; }
    public int page_count { get; set; }
    public int year_published { get; set; }

}
