using System.ComponentModel.DataAnnotations;

namespace SendingEmail_App.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Логин обязателен")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Код подтверждения")]
        public string ConfirmationCode { get; set; }
    }
}
