using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROJECT.Data;
using PROJECT.Models;

namespace PROJECT.Controllers
{
    public class ProjectImagesController : Controller
    {
        private CustomerContext _dbContext;

        public ProjectImagesController(CustomerContext dbContext)
        {
            // injecting dependency
            _dbContext = dbContext;
        }

        Projects? GetProject(int id) => _dbContext.Projects.ToList().FirstOrDefault(p => p.Id.Equals(id));
        Customer? GetCustomer(int id) => _dbContext.Customers.ToList().FirstOrDefault(c => c.Id.Equals(id));
        ProjectImages? GetImages(int id) => _dbContext.ProjectImages.ToList().FirstOrDefault(c => c.Id.Equals(id));

        [HttpGet]
        [Authorize]
        public IActionResult ProjectPhotos(int id)
        {

            var imgList = from i in _dbContext.ProjectImages
                          where i.ProjectId == id
                          select i;

            List<string> imageUrls = new();

            foreach (ProjectImages img in imgList)
            {
                string toBase64 = Convert.ToBase64String(img.Images);
                string imageData = string.Format("data:image/jpg;base64,{0}", toBase64);
                imageUrls.Add(imageData);
            }
            ViewBag.ProjId = id;
            ViewBag.Images = imageUrls;

            return View();
        }


        [HttpPost]
        [Authorize]
        public IActionResult AddImage(ProjectImages img)
        {
            if (img == null) RedirectToAction("ProjectPhotos", img.ProjectId);

            foreach (var file in Request.Form.Files)
            {
                MemoryStream ms = new();
                file.CopyTo(ms);
                img.Images = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }

            img.Projects = GetProject(img.ProjectId);

            _dbContext.ProjectImages.Add(img);
            _dbContext.SaveChanges();

            return RedirectToAction("ProjectPhotos", new { id = img.ProjectId});
        }
    }
}
