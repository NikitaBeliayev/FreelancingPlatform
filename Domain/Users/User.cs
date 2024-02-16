using Domain.Objectives;
using Domain.Roles;
using Domain.UserCommunicationChannels;
using Shared;

namespace Domain.Users.UserDetails
{
    public class User : Entity
    {
        public EmailAddress Email { get; set; }
        public Name FirstName { get; set; }
        public Name LastName { get; set; }
        public Password Password { get; set; }
        public ICollection<Role> Roles { get; } = new List<Role>();
        public ICollection<Objective> Objectives { get; } = new List<Objective>();

        public ICollection<UserCommunicationChannel> CommunicationChannels { get; set; } =
            new List<UserCommunicationChannel>();

        public User() : base(Guid.NewGuid())
        {
        }

        public User(Guid id, EmailAddress email, Name firstName, Name lastName, Password password,
            ICollection<UserCommunicationChannel> userCommunicationChannels,  ICollection<Role> rolesCollection) : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            CommunicationChannels = userCommunicationChannels;
            Roles = rolesCollection;
        }
    }
}
