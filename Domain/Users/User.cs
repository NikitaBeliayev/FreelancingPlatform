using Domain.Objectives;
using Domain.Roles;
using Domain.UserCommunicationChannels;
using Domain.Users.UserDetails;
using Shared;

namespace Domain.Users
{
    public class User : Entity
    {
        public Email Email { get; set; }
        public Name FirstName { get; set; }
        public Name LastName { get; set; }
        public Password Password { get; set; }
        public ICollection<Role> Roles { get; } = new List<Role>();
        public ICollection<Objective> ObjectivesToImplement { get; } = new List<Objective>();
        
        public ICollection<Objective> CreatedObjectives { get; } = new List<Objective>();

        public ICollection<UserCommunicationChannel> CommunicationChannels { get; set; } =
            new List<UserCommunicationChannel>();

        public User(Guid id) : base(id)
        {
        }

        public User(Guid id, Email email, Name firstName, Name lastName, Password password,
            ICollection<UserCommunicationChannel> userCommunicationChannels,  ICollection<Role> rolesCollection, ICollection<Objective> objectiveToImplement, 
            ICollection<Objective> createdObjectives) : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            CommunicationChannels = userCommunicationChannels;
            Roles = rolesCollection;
            ObjectivesToImplement = objectiveToImplement;
            CreatedObjectives = createdObjectives;
        }
    }
}
