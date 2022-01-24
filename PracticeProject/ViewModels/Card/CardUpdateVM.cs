using Microsoft.AspNetCore.Http;

namespace PracticeProject.ViewModels.Card
{
    public class CardUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public IFormFile Photo { get; set; }
    }
}
