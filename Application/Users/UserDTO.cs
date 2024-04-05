using System.Text.Json.Serialization;

namespace Application.Users
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [JsonIgnore]
        public string Password { get; set; } = string.Empty;

        public UserDto()
        {
        }

        public UserDto(Guid? id, string email, string firstName, string lastName, string password)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }
    }
}
