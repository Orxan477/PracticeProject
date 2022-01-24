using System.ComponentModel.DataAnnotations;

namespace PracticeProject.ViewModels.Account
{
    public class RegisterVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string RePassword { get; set; }
    }
}
