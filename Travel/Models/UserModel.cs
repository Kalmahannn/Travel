using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Key]
    [Required(ErrorMessage = "Логин қажет")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Құпия сөз қажет")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Құпия сөзді қайталаңыз")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Құпия сөздер сәйкес емес")]
    public string ConfirmPassword { get; set; }
}
