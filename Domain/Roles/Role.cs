using Domain.Users.UserDetails;

namespace Domain.Roles;

public class Role
{
    public int Id { get; set; }
    public RoleName Name { get; set; }
    public ICollection<User> Users = new List<User>();
    public Role()
    {
        
    }
    public Role(int id, RoleName name, ICollection<User> usersCollection)
    {
        Id = id;
        Name = name;
        Users = usersCollection;
    }
}