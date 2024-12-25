namespace DoMain.Entities;

public class Book
{
  public int Id { get; set; }
  public string Title { get; set; }
  public int AuthorId { get; set; }
  public int PublisherYear { get; set; }
  public string Genre { get; set; }
  public bool IsAvialable { get; set; }
}