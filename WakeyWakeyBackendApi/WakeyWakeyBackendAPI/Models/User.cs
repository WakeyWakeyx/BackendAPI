using System.ComponentModel.DataAnnotations;

namespace WakeyWakeyBackendAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email format")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

        public required string AccessToken { get; set; }
    }
}
