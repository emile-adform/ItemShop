using System.ComponentModel.DataAnnotations;

namespace ItemShop.Models.DTOs.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }
        public string Username { get; set; }

    }
}
