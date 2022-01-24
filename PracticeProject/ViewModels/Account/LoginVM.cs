using System.ComponentModel.DataAnnotations;

namespace PracticeProject.ViewModels.Account
{
    public class LoginVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
