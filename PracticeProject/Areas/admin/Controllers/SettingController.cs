using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.DAL;
using PracticeProject.Models;
using PracticeProject.ViewModels.Settings;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeProject.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public SettingController(AppDbContext context,
                                 IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(_context.Settings.Where(x => !x.IsDeleted).ToList());
        }
        public IActionResult Update(int id)
        {
            Settings setting = _context.Settings.Where(x => x.IsDeleted == false && x.Id == id).FirstOrDefault();
            if (setting == null) return NotFound();
            SettingUpdateVM updateVM = _mapper.Map<SettingUpdateVM>(setting);
            return View(updateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, SettingUpdateVM settingUpdate)
        {
            if (!ModelState.IsValid) return View(settingUpdate);
            Settings dbSetting = _context.Settings.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (dbSetting == null) return NotFound();
            bool isExistCurrentValue = dbSetting.Value.Trim().ToLower() == settingUpdate.Value.Trim().ToLower();
            if (!isExistCurrentValue)
            {
                dbSetting.Value = settingUpdate.Value;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
