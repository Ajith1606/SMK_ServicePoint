using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_ServicePoint.Data;
using SMK_ServicePoint.Models;

namespace SMK_ServicePoint.Controllers
{
    public class AadharcardController : Controller
    {
        private readonly LoginDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AadharcardController(LoginDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Aadhar> aadhars = _context.aadharsCards.ToList();
            return View(aadhars);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Aadhar aadhar)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                //PhotoImageUrl
                if (file.Count > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/AadharCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, newfilename + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filestream);
                    }

                    aadhar.PhotoImageUrl = @"\Image\AadharCardClient\" + newfilename + extension;
                }

                if (ModelState.IsValid)
                {
                    _context.aadharsCards.Add(aadhar);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(aadhar);
            }
            catch (Exception ex) 
            {
                throw new Exception("Something Wrong" + ex.Message);
            }
          
        }

        public ActionResult Details(int id)
        {
            Aadhar aadhar = _context.aadharsCards.FirstOrDefault(a => a.Id == id);
            return View(aadhar);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Aadhar aadhar = _context.aadharsCards.FirstOrDefault(a => a.Id == id);
            return View(aadhar);
        }

        [HttpPost]
        public ActionResult Edit(Aadhar aadhar)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                //PhotoImageUrl
                if (file.Count() > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/AadharCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    //Delete
                    var objFromDb = _context.aadharsCards.AsNoTracking().FirstOrDefault(x => x.Id == aadhar.Id);

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

                    aadhar.PhotoImageUrl = @"\Image\AadharCardClient\" + newfilename + extension;
                }



                if (ModelState.IsValid)
                {
                    var objFromDb = _context.aadharsCards.AsNoTracking().FirstOrDefault(x => x.Id == aadhar.Id);

                    objFromDb.TamilName = aadhar.TamilName;
                    objFromDb.Name = aadhar.Name;
                    objFromDb.Gender = aadhar.Gender;
                    objFromDb.TamilAddress = aadhar.TamilAddress;
                    objFromDb.Address = aadhar.Address;
                    objFromDb.Aadharno1 = aadhar.Aadharno1;
                    objFromDb.Aadharno2 = aadhar.Aadharno2;
                    objFromDb.Aadharno3 = aadhar.Aadharno3;

                    if (aadhar.PhotoImageUrl != null)
                    {
                        objFromDb.PhotoImageUrl = aadhar.PhotoImageUrl;
                    }

                    _context.aadharsCards.Update(objFromDb);
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
            Aadhar aadhar = _context.aadharsCards.FirstOrDefault(a => a.Id == id);
            return View(aadhar);
        }

        [HttpPost]
        public ActionResult Delete(Aadhar aadhar)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;

                //PhotoImageUrl
                if (!string.IsNullOrEmpty(aadhar.PhotoImageUrl))
                {
                    //Delete
                    var objFromDb = _context.aadharsCards.AsNoTracking().FirstOrDefault(x => x.Id == aadhar.Id);

                    if (objFromDb.PhotoImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webrootpath, objFromDb.PhotoImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }

                _context.aadharsCards.Remove(aadhar);
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
