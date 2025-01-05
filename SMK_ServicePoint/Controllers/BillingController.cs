using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMK_ServicePoint.Data;
using SMK_ServicePoint.Models;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using Session = Stripe.Checkout.Session;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;
using SessionService = Stripe.Checkout.SessionService;

namespace SMK_ServicePoint.Controllers
{
    public class BillingController : Controller
    {
        private readonly LoginDbContext _context;
        private readonly string _stripeSecretKey;
        public BillingController(LoginDbContext context, IConfiguration configuration)
        {
            _context = context;
            _stripeSecretKey = configuration["Stripe:SecretKey"];
        }

        public ActionResult Billing()
        {
            return View();
        }
        public IActionResult Invoice(int id)
        {
            var billing = _context.Billings.Include(b => b.Customer).FirstOrDefault(b => b.Id == id);

            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }


        [HttpGet]
        public ActionResult Index()
        {
            var billings = _context.Billings.Include(b => b.Customer).ToList();
            return View(billings);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers.ToList(), "Id", "CustomerName");
            return View();
        }


        [HttpPost]
        public ActionResult Create(Billing billing)
        {

            if (ModelState.IsValid)
            {

                billing.Amount = Convert.ToDouble(billing.Quantity * billing.UnitPrice);
                billing.Tax = billing.Tax * billing.UnitPrice;
                billing.Total = billing.Amount + billing.Tax;

                _context.Billings.Add(billing);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(billing);
        }

        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();

            Session session = service.Get(TempData["Session"].ToString());

            if (session.PaymentStatus == "paid")
            {
                var transaction = session.PaymentIntentId.ToString();
                return View("Success");
            }

            return View("Login");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult CheckOut(int id)
        {
            try
            {
                StripeConfiguration.ApiKey = _stripeSecretKey;

                var selectedBilling = _context.Billings
                    .Include(b => b.Customer)
                    .FirstOrDefault(b => b.Id == id);

                if (selectedBilling == null)
                {
                    return NotFound();
                }

                var totalAmount = (selectedBilling.UnitPrice * selectedBilling.Quantity) + selectedBilling.Tax;
              
                if (totalAmount < 50)
                {
                    return BadRequest("The total amount must be at least ₹50.");
                }

                var domain = "https://localhost:7223/";
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + "Billing/OrderConfirmation",
                    CancelUrl = domain + "Billing/Login",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment"
                };

                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(((totalAmount*50)/10)*2), 
                        Currency = "inr", 
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = selectedBilling.Service.ToString()
                        }
                    },
                    Quantity = selectedBilling.Quantity
                };

                options.LineItems.Add(sessionListItem);

                var service = new SessionService();
                Session session = service.Create(options);

                TempData["Session"] = session.Id;
                Response.Headers.Append("Location", session.Url);

                return new StatusCodeResult(303);
            }
            catch (StripeException stripeEx)
            {
                Console.WriteLine($"Stripe error: {stripeEx.Message}");
                return StatusCode(500, "Stripe API error while processing the checkout.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Stripe session: {ex.Message}");
                return StatusCode(500, "Internal server error while processing the checkout.");
            }
        }

    }
}
