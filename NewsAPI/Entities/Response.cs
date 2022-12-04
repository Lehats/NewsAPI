namespace NewsAPI.Entities;

public class Response<T>
    where T : class
{
    public int Count { get; set; }
    public List<T> Articles { get; set; } = new List<T>();
}