using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMK_ServicePoint.Data;
using SMK_ServicePoint.Models;

namespace SMK_ServicePoint.Controllers
{
    public class CustomerController : Controller
    {
        private readonly LoginDbContext _context;
        public CustomerController(LoginDbContext context) 
        { 
            _context = context;
        }
        public ActionResult Index()
        {
            List<Customer> customers = _context.Customers.ToList();
            return View(customers);
        }

        [HttpGet]
        public ActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Customers.Add(customer);
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(customer);
            }
            catch (Exception ex)
            {
                throw new Exception("Something Wrong" + ex.Message);
            }
           
        }

        public ActionResult Details(int id)
        {
            Customer customer = _context.Customers.FirstOrDefault(a => a.Id == id);
            return View(customer);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.FirstOrDefault(a => a.Id == id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objFromDb = _context.Customers.AsNoTracking().FirstOrDefault(x => x.Id == customer.Id);

                    objFromDb.CustomerName = customer.CustomerName;
                    objFromDb.Email = customer.Email;
                    objFromDb.Phone = customer.Phone;

                    _context.Customers.Update(objFromDb);
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
            Customer customer = _context.Customers.FirstOrDefault(a => a.Id == id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Delete(Customer customer)
        {           
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
