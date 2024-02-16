using Domain.Users.Errors;
using Shared;
using System.Text.RegularExpressions;

namespace Domain.Users.UserDetails
{
    public class Photo
    {
        public string Value { get; }

        private Photo(string value) => Value = value;

        public static Result<Photo> BuildPhoto(string base64Data)
        {
            if (!IsBase64String(base64Data))
            {
                return Result<Photo>.Failure(null, PhotoErrors.InvalidBase64Format);
            }

            return Result<Photo>.Success(new Photo(base64Data));
        }
        
        /// <summary>
        /// Use this method only for ef core configuration
        /// </summary>
        /// <param name="base64Data"></param>
        /// <returns></returns>
        public static Result<Photo> BuildPhotoWithoutValidation(string base64Data)
        {
            return Result<Photo>.Success(new Photo(base64Data));
        }

        private static bool IsBase64String(string s)
        {
            string base64Pattern = @"^[A-Za-z0-9+/]+={0,2}$";

            return Regex.IsMatch(s, base64Pattern, RegexOptions.None);
        }
    }
}