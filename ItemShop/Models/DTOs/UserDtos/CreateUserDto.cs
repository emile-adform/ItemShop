using System.ComponentModel.DataAnnotations;

namespace ItemShop.Models.DTOs.UserDtos
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public string Website { get; set; }

    }
}
