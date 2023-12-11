using System.ComponentModel.DataAnnotations;
using Humanizer.Localisation.TimeToClockNotation;
using LibraryCore.Models;
using LibraryCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Admin
{
    public class CategoryManagementModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManagementModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Category> Categories { get; set; }
        [BindProperty]
        public Category Category { get; set; }

        [Required]
        public IFormFile Image { get; set; }
        public void OnGet()
        {
            Categories = _unitOfWork.CategoryRepository.GetAll().ToList();
        }

        public IActionResult OnPostEditCategory()
        {
            Category c = _unitOfWork.CategoryRepository.Find(int.Parse(Request.Form["id"]));
            c.CategoryName = Request.Form["name"];
            var file = Request.Form.Files["newimage"];

            if (file != null && file.Length > 0)
            {
                var fileName = file.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "category", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                c.Image = "/img/category/" + fileName;

            }
            _unitOfWork.CategoryRepository.Update(c);
            _unitOfWork.SaveChange();
            return RedirectToPage();
        }

        public IActionResult OnPostAddCategory()
        {
            var fileName = Image.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "category", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(stream);
            }
            Category.Image = "/img/category/" + fileName;
            _unitOfWork.CategoryRepository.Add(Category);
            _unitOfWork.SaveChange();
            return RedirectToPage();
        }

        public IActionResult OnGetDeleteCategory(int categoryId)
        {
            try
            {
                _unitOfWork.CategoryRepository.Delete(categoryId);
                _unitOfWork.SaveChange();
                return new JsonResult("1");

            }
            catch (Exception e)
            {
                return new JsonResult("0");

            }

        }
    }
}
