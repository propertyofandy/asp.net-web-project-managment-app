using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECT.Data;
using PROJECT.Models;

namespace PROJECT.Controllers
{
    public class HomeController : Controller 
    {
        private CustomerContext _dbContext;

        public HomeController(CustomerContext dbContext)
        {
            // injecting dependency
            _dbContext = dbContext;
        }
        [AllowAnonymous]
        public IActionResult Gallery()
        {
            if (_dbContext.ProjectImages.ToList() == null) { return View(); }

            var imgList = from i in _dbContext.ProjectImages
                          where i.hasPublicPermision == true
                          select i;

            List<string> imageUrls = new();

            foreach (ProjectImages img in imgList)
            {
                string toBase64 = Convert.ToBase64String(img.Images);
                string imageData = string.Format("data:image/jpg;base64,{0}", toBase64);
                imageUrls.Add(imageData);
            }
            ViewBag.Images = imageUrls;
            return View();
        }

        [AllowAnonymous]
        public ViewResult HomeView() => View();

        [AllowAnonymous]
        public ViewResult Services() => View();


        [AllowAnonymous]
        public ViewResult Error() => View();

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Add(ProjectImages img)
        {
            if(img == null) RedirectToAction("HomeView");

            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new(); 
                file.CopyTo(ms);
                img.Images = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }
            _dbContext.ProjectImages.Add(img);
            _dbContext.SaveChanges();
            
            return RedirectToAction("HomeView");
        }
    }
}
