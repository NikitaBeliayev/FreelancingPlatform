namespace Application.Users
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserDto()
        {
        }

        public UserDto(Guid? id, string email, string firstName, string lastName, string password)
        {
            Id = id;
            EmailAddress = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }
    }
}
