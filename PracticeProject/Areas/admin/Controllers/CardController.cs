using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeProject.DAL;
using PracticeProject.Models;
using PracticeProject.Utilities;
using PracticeProject.ViewModels.Card;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeProject.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CardController : Controller
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private IWebHostEnvironment _env;
        private string _errorMessage;
        private int _size;
        
        public CardController(AppDbContext context,
                              IMapper mapper,
                              IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
            _size = Settings("Key");
        }
        private int Settings(string key)
        {
            string dbSetting = _context.Settings.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault();
            int value = int.Parse(dbSetting);
            return value;
        }
        public IActionResult Index()
        {
            return View(_context.Cards.Where(p=>p.IsDeleted==false).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CardCreateVM cardCreate)
        {
            if (!ModelState.IsValid) return View(cardCreate);
            bool isExistName = await _context.Cards.AnyAsync(x => x.Name.Trim().ToLower() == 
                                                                                cardCreate.Name.Trim().ToLower());
            if (isExistName)
            {
                ModelState.AddModelError("Name", "Multiple Name");
                return View(cardCreate);
            }
            if (!CheckImageValid(cardCreate.Photo,"image/",_size))
            {
                ModelState.AddModelError("Photo", _errorMessage);
                return View(cardCreate);
            }
            string fileName=await Extension.SaveFileAsync(cardCreate.Photo, _env.WebRootPath, "assets/img");

            //Card card = _mapper.Map<Card>(cardCreate);
            Card card = new Card
            {
                Name = cardCreate.Name,
                Content = cardCreate.Content,
                Image = fileName
            };
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CheckImageValid(IFormFile file,string type,int size)
        {
            if (!Extension.CheckSize(file,size))
            {
                _errorMessage = "size>200";
                return false;
            }
            if (!Extension.CheckType(file, type))
            {
                _errorMessage = "Type is not supported";
                return false;
            }
            return true;
        }
        public IActionResult Update(int id)
        {
            Card card =  _context.Cards.Where(x => x.IsDeleted == false && x.Id == id).FirstOrDefault();
            if (card == null) return NotFound();
            CardUpdateVM updateVM = _mapper.Map<CardUpdateVM>(card);
            return View(updateVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,CardUpdateVM cardUpdate)
        {
            if (!ModelState.IsValid) return View(cardUpdate);
            Card dbCard = _context.Cards.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (dbCard == null) return NotFound();
            bool isExistNameDb = await _context.Cards.AnyAsync(x => x.Name.Trim().ToLower() ==
                                                                      cardUpdate.Name.Trim().ToLower());
            bool isExistCurrentName = dbCard.Name.Trim().ToLower() == cardUpdate.Name.Trim().ToLower();

            if (isExistNameDb && !isExistCurrentName)
            {
                ModelState.AddModelError("Name", "Multiple Name");
                return View(cardUpdate);
            }
            if (!isExistCurrentName)
            {
                dbCard.Name = cardUpdate.Name;
            }
            bool ifExistCurrentContent= dbCard.Content.Trim().ToLower() == cardUpdate.Content.Trim().ToLower();
            if (!ifExistCurrentContent)
            {
                dbCard.Content = cardUpdate.Content;
            }
            if (cardUpdate.Photo!=null)
            {
                if (!CheckImageValid(cardUpdate.Photo, "image/", _size))
                {
                    ModelState.AddModelError("Photo", _errorMessage);
                    return View(cardUpdate);
                }
                Helper.RemoveFile(_env.WebRootPath, "assets/img", dbCard.Image);
                string fileName = await Extension.SaveFileAsync(cardUpdate.Photo, _env.WebRootPath, "assets/img");
                dbCard.Image = fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Card dbCard = _context.Cards.Where(x => !x.IsDeleted && x.Id == id).FirstOrDefault();
            if (dbCard is null) return NotFound();
            dbCard.IsDeleted = true;
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
