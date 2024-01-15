using Domain.Users;
using Domain.Users.UserDetails;

namespace Domain.Roles;

public class Role
{
    public int Id { get; set; }
    public RoleNames Name { get; set; }
    public ICollection<User> Users = new List<User>();
    public Role()
    {
        
    }
    public Role(int id, RoleNames name, ICollection<User> usersCollection)
    {
        Id = id;
        Name = name;
        Users = usersCollection;
    }
}