using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_ServicePoint.Data;
using SMK_ServicePoint.Models;

namespace SMK_ServicePoint.Controllers
{
    public class VotercardController : Controller
    {
        private readonly LoginDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VotercardController(LoginDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<Voter> voters = _context.voterCards.ToList();
            return View(voters);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Voter voter)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                //PhotoImageUrl
                if (file.Count > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/VoterCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, newfilename + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filestream);
                    }

                    voter.PhotoImageUrl = @"\Image\VoterCardClient\" + newfilename + extension;
                }

                //SignImageUrl
                if (file.Count > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/VoterCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, newfilename + extension), FileMode.Create))
                    {
                        file[0].CopyTo(filestream);
                    }

                    voter.SignImageUrl = @"\Image\VoterCardClient\" + newfilename + extension;
                }
                if (ModelState.IsValid)
                {
                    _context.voterCards.Add(voter);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(voter);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Wrong" + ex.Message);
            }
           
        }
        public ActionResult Details(int id)
        {
            Voter voter = _context.voterCards.FirstOrDefault(x => x.Id == id);
            return View(voter);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Voter voter = _context.voterCards.FirstOrDefault(x => x.Id == id);
            return View(voter);
        }

        [HttpPost]
        public ActionResult Edit(Voter voter)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                //PhotoImageUrl
                if (file.Count() > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/VoterCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    //Delete
                    var objFromDb = _context.voterCards.AsNoTracking().FirstOrDefault(x => x.Id == voter.Id);

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

                    voter.PhotoImageUrl = @"\Image\VoterCardClient\" + newfilename + extension;
                }

                //SignImageUrl
                if (file.Count() > 0)
                {
                    string newfilename = Guid.NewGuid().ToString();

                    var upload = Path.Combine(webrootpath, @"Image/VoterCardClient");

                    var extension = Path.GetExtension(file[0].FileName);

                    //Delete
                    var objFromDb = _context.voterCards.AsNoTracking().FirstOrDefault(x => x.Id == voter.Id);

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

                    voter.SignImageUrl = @"\Image\VoterCardClient\" + newfilename + extension;
                }

                if (ModelState.IsValid)
                {
                    var objFromDb = _context.voterCards.AsNoTracking().FirstOrDefault(x => x.Id == voter.Id);

                    objFromDb.EpicNo = voter.EpicNo;
                    objFromDb.TamilName = voter.Name;
                    objFromDb.Name = voter.Name;
                    objFromDb.Title = voter.Title;
                    objFromDb.TamilFatherNameorHusbandName = voter.TamilFatherNameorHusbandName;
                    objFromDb.FatherNameorHusbandName = voter.FatherNameorHusbandName;
                    objFromDb.Gender = voter.Gender;
                    objFromDb.DateofBirth = voter.DateofBirth;
                    objFromDb.TamilAddress = voter.TamilAddress;
                    objFromDb.Address = voter.Address;
                    objFromDb.TamilElectoralRegistrationOfficier = voter.TamilElectoralRegistrationOfficier;
                    objFromDb.ElectoralRegistrationOfficier = voter.ElectoralRegistrationOfficier;
                    objFromDb.DownloadDate = voter.DownloadDate;

                    if (voter.PhotoImageUrl != null && voter.SignImageUrl != null)
                    {
                        objFromDb.PhotoImageUrl = voter.PhotoImageUrl;
                        objFromDb.SignImageUrl = voter.SignImageUrl;
                    }

                    _context.voterCards.Update(objFromDb);
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
            Voter voter = _context.voterCards.FirstOrDefault(x => x.Id == id);
            return View(voter);
        }

        [HttpPost]
        public ActionResult Delete(Voter voter)
        {
            try
            {
                string webrootpath = _webHostEnvironment.WebRootPath;

                //PhotoImageUrl
                if (!string.IsNullOrEmpty(voter.PhotoImageUrl))
                {
                    //Delete
                    var objFromDb = _context.voterCards.AsNoTracking().FirstOrDefault(x => x.Id == voter.Id);

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
                if (!string.IsNullOrEmpty(voter.SignImageUrl))
                {
                    //Delete
                    var objFromDb = _context.voterCards.AsNoTracking().FirstOrDefault(x => x.Id == voter.Id);

                    if (objFromDb.SignImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(webrootpath, objFromDb.SignImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }
                _context.voterCards.Remove(voter);
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
