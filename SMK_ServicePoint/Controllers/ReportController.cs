using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMK_ServicePoint.Data;
using SMK_ServicePoint.Models;

namespace SMK_ServicePoint.Controllers
{
    public class ReportController : Controller
    {
        private readonly LoginDbContext _context;
        public ReportController(LoginDbContext context) 
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var reports = _context.Reports.Include(r => r.Customer).ToList();
            return View(reports);          
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers.ToList(), "Id", "CustomerName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Report report)
        {
         
            if (ModelState.IsValid)
            {
               
                _context.Reports.Add(report);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(report);
        }
        public ActionResult Details(int id)
        {
            Report report = _context.Reports.FirstOrDefault(a => a.Id == id);
            return View(report);
        }

        public ActionResult Delete(int id)
        {
            Report report = _context.Reports.FirstOrDefault(a => a.Id == id);
            return View(report);
        }

        [HttpPost]
        public ActionResult Delete(Report report)
        {
            _context.Reports.Remove(report);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
