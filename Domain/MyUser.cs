using System.Collections.Generic;

namespace AspnetOkta.Domain
{
  public class MyUser{
    public MyUser()
    { 
      this.CustomData = new CustomData();
      this.Groups = new List<Group>();  
    }
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FavoriteColor { get; set; }
    public CustomData CustomData { get; set; }
    public IList<Group> Groups { get; set; }
  }
}