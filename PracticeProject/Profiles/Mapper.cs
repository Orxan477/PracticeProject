using AutoMapper;
using PracticeProject.Models;
using PracticeProject.ViewModels.Card;
using PracticeProject.ViewModels.Settings;

namespace PracticeProject.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Card, CardUpdateVM>();
            CreateMap<Settings, SettingUpdateVM>();
        }
    }
}
