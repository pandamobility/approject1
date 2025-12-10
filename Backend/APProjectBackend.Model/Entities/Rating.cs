namespace APProjectBackend.Model.Entities;
public class Rating
{
    public Rating(int rating_id) { Rating_id = rating_id; }
    public int Rating_id { get; set; }
    public int rating { get; set; }

}
