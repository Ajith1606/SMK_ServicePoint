using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_ServicePoint.Data;
using SMK_ServicePoint.Models;

namespace SMK_ServicePoint.Controllers
{
    public class PancardController : Controller
    {
        private readonly LoginDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PancardController(LoginDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<PanCard> pancards = _context.panCards.ToList();
            return View(pancards);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PanCard panCard)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                //PhotoImageUrl
                if (file.Count > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/PanCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, newfilename + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filestream);
                    }

                    panCard.PhotoImageUrl = @"\Image\PanCardClient\" + newfilename + extension;
                }

                //SignImageUrl
                if (file.Count > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/PanCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, newfilename + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filestream);
                    }

                    panCard.SignImageUrl = @"\Image\PanCardClient\" + newfilename + extension;
                }

                if (ModelState.IsValid)
                {
                    _context.panCards.Add(panCard);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(panCard);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Wrong" + ex.Message);
            }
            
        }

        public ActionResult Details(int id)
        {
            PanCard panCard = _context.panCards.FirstOrDefault(x => x.Id == id);
            return View(panCard);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PanCard panCard = _context.panCards.FirstOrDefault(x => x.Id == id);
            return View(panCard);
        }

        [HttpPost]
        public ActionResult Edit(PanCard panCard)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                //PhotoImageUrl
                if (file.Count() > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/PanCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    //Delete
                    var objFromDb = _context.panCards.AsNoTracking().FirstOrDefault(x => x.Id == panCard.Id);

                    if (objFromDb.PhotoImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webrootpath, objFromDb.PhotoImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(upload, newfilename + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filestream);
                    }

                    panCard.PhotoImageUrl = @"\Image\PanCardClient\" + newfilename + extension;
                }

                //SignImageUrl
                if (file.Count() > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/PanCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    //Delete
                    var objFromDb = _context.panCards.AsNoTracking().FirstOrDefault(x => x.Id == panCard.Id);

                    if (objFromDb.SignImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webrootpath, objFromDb.SignImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(upload, newfilename + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filestream);
                    }

                    panCard.SignImageUrl = @"\Image\PanCardClient\" + newfilename + extension;
                }

                if (ModelState.IsValid)
                {
                    var objFromDb = _context.panCards.AsNoTracking().FirstOrDefault(x => x.Id == panCard.Id);

                    objFromDb.PanNo = panCard.PanNo;
                    objFromDb.Name = panCard.Name;
                    objFromDb.FatherName = panCard.FatherName;

                    if (panCard.PhotoImageUrl != null && panCard.SignImageUrl != null)
                    {
                        objFromDb.PhotoImageUrl = panCard.PhotoImageUrl;
                        objFromDb.SignImageUrl = panCard.SignImageUrl;
                    }

                    _context.panCards.Update(objFromDb);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("Something Wrong" + ex.Message);
            }
            
            
        }


        public ActionResult Delete(int id)
        {
            PanCard panCard = _context.panCards.FirstOrDefault(x => x.Id == id);
            return View(panCard);
        }


        [HttpPost]
        public ActionResult Delete(PanCard panCard)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;

                //PhotoImageUrl
                if (!string.IsNullOrEmpty(panCard.PhotoImageUrl))
                {
                    //Delete
                    var objFromDb = _context.panCards.AsNoTracking().FirstOrDefault(x => x.Id == panCard.Id);

                    if (objFromDb.PhotoImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webrootpath, objFromDb.PhotoImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }

                //SignImageUrl
                if (!string.IsNullOrEmpty(panCard.SignImageUrl))
                {
                    //Delete
                    var objFromDb = _context.panCards.AsNoTracking().FirstOrDefault(x => x.Id == panCard.Id);

                    if (objFromDb.SignImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webrootpath, objFromDb.SignImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }
                _context.panCards.Remove(panCard);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception("Something Wrong" + ex.Message);
            }
           
        }

    }
}
