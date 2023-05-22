using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace JewelryShop.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword")]
        public string ConfirmedPassword { get; set; } = string.Empty;


    }
}