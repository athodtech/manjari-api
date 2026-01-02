using System.ComponentModel.DataAnnotations;

namespace AthodBeTrackApi.Models
{
    public class SingIn
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public bool ForceLogin { get; set; } = false;
    }
}
