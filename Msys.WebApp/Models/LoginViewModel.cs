using System.ComponentModel.DataAnnotations;

namespace Msys.WebApp.Models
{
    public class LoginViewModel
    {
        public Login LoginInfo { get; set; }
        public Register RegisterInfo { get; set; }

        public LoginViewModel()
        {
            LoginInfo = new Login();
            RegisterInfo = new Register();
        }
        public class Login
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Nhớ mật khẩu?")]
            public bool RememberMe { get; set; }
        }

        public class Register
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public bool RePassword { get; set; }
        }

    }
}