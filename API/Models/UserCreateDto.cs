using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Index(nameof(Login), IsUnique = true)]

    public class UserCreateDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int UserGroupId { get; set; }
    }
}
