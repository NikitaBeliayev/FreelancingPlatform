using Domain.Users;
using Domain.Users.UserDetails;
using Shared;

namespace Domain.Roles;

public class Role : Entity
{
    public RoleName Name { get; set; }
    public ICollection<User> Users = new List<User>();
    public Role(Guid id) : base(id)
    {
        
    }
    public Role(Guid id, RoleName name, ICollection<User> usersCollection) : base(id)
    {
        Name = name;
        Users = usersCollection;
    }
}