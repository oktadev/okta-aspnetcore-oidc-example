using System.Linq;
using System.Threading.Tasks;
using AspnetOkta.Domain;
using Newtonsoft.Json;
using Okta.Sdk;

namespace AspnetOkta.Mapper
{
  public static class Map
  {
    public async static Task<MyUser> ToMyUser(IUser user)
    {
      var returnUser = new MyUser();
      returnUser.Id = user.Id;
        returnUser.FirstName = user.Profile["firstName"].ToString();
        returnUser.LastName = user.Profile["lastName"].ToString();
        if(user.Profile["favoriteColor"] != null){
          returnUser.FavoriteColor = user.Profile["favoriteColor"].ToString();
        }
      if(user.Profile["custom_data"] != null){
        returnUser.CustomData = JsonConvert.DeserializeObject<CustomData>(user.Profile["custom_data"].ToString());
      }

      var groups = await user.Groups.ToList();
      foreach (var group in groups)
      {
        returnUser.Groups.Add(new Domain.Group
        {
          Id = group.Id,
          Name = group.Profile.Name,
          Description = group.Profile.Description
        });
      }

      return returnUser;
    }
  }
}