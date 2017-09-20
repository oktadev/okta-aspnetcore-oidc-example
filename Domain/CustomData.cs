namespace AspnetOkta.Domain
{
  public class CustomData
  {
    public CustomData()
    {
        this.Favorites = new Favorite[0];
    }
    public Favorite[] Favorites { get; set; }
  }
}