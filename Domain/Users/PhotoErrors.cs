using Shared;

namespace Domain.Users
{
    public static class PhotoErrors
    {
        public static Error InvalidBase64Format =>
            new("Users.Photo.InvalidBase64Format", "Invalid Base64 format for the photo.", 422);
    }
}