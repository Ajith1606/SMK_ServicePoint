using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_ServicePoint.Data;
using SMK_ServicePoint.Models;

namespace SMK_ServicePoint.Controllers
{
    public class SmartcardController : Controller
    {
        private readonly LoginDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SmartcardController(LoginDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<SmartCard> smartcards = _context.smartCards.ToList();
            return View(smartcards);
        }

   
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(SmartCard model)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                //PhotoImageUrl
                if (file.Count > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/SmartCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, newfilename + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filestream);
                    }

                    model.PhotoImageUrl = @"\Image\SmartCardClient\" + newfilename + extension;
                }
                if (ModelState.IsValid)
                {
                    // Save SmartCard
                    _context.smartCards.Add(model);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Wrong" + ex.Message);
            }
            
        }

        public ActionResult Details(int id)
        {
            SmartCard smartCard = _context.smartCards.FirstOrDefault(x=>x.Id == id);
            return View(smartCard);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SmartCard smartCard = _context.smartCards.FirstOrDefault(x => x.Id == id);
            return View(smartCard);
        }

        [HttpPost]
        public ActionResult Edit(SmartCard smartCard)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                //PhotoImageUrl
                if (file.Count() > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/SmartCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    //Delete
                    var objFromDb = _context.smartCards.AsNoTracking().FirstOrDefault(x => x.Id == smartCard.Id);

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

                    smartCard.PhotoImageUrl = @"\Image\SmartCardClient\" + newfilename + extension;
                }
                if (ModelState.IsValid)
                {
                    var objFromDb = _context.smartCards.AsNoTracking().FirstOrDefault(x => x.Id == smartCard.Id);

                    objFromDb.FamilyHead = smartCard.FamilyHead;
                    objFromDb.FatherOrHusband = smartCard.FatherOrHusband;
                    //objFromDb.DateofBirth = smartCard.smartcards.DateofBirth;
                    objFromDb.Address = smartCard.Address;
                    objFromDb.CardType = smartCard.CardType;
                    objFromDb.CardNumber = smartCard.CardNumber;
                    objFromDb.ShopNumber = smartCard.ShopNumber;
                    objFromDb.year = smartCard.year;

                    if (smartCard.PhotoImageUrl != null)
                    {
                        objFromDb.PhotoImageUrl = smartCard.PhotoImageUrl;
                    }

                    _context.smartCards.Update(objFromDb);
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
            SmartCard smartCard = _context.smartCards.FirstOrDefault(x => x.Id == id);
            return View(smartCard);
        }


        [HttpPost]   
        public ActionResult Delete(SmartCard smartCard)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;
                //PhotoImageUrl
                if (!string.IsNullOrEmpty(smartCard.PhotoImageUrl))
                {
                    //Delete
                    var objFromDb = _context.smartCards.AsNoTracking().FirstOrDefault(x => x.Id == smartCard.Id);

                    if (objFromDb.PhotoImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webrootpath, objFromDb.PhotoImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }
                _context.smartCards.Remove(smartCard);
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
