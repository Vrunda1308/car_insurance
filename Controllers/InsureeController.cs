using Car_Insurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourNamespace.Models;

namespace Car_Insurance.Controllers
{
    public class InsureeController : System.Web.Mvc.Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Insuree
        public System.Web.Mvc.ActionResult Index() => View(db.Insurees.ToList());

        private ActionResult View(object p)
        {
            throw new NotImplementedException();
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Models.Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = CalculateQuote(insuree);
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // Additional methods for Edit, Details, Delete would go here

        private decimal CalculateQuote(Insuree insuree)
        {
            decimal baseQuote = 50;

            int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
            if (DateTime.Now.DayOfYear < insuree.DateOfBirth.DayOfYear)
                age--;

            if (age <= 18)
                baseQuote += 100;
            else if (age <= 25)
                baseQuote += 50;
            else
                baseQuote += 25;

            if (insuree.CarYear < 2000)
                baseQuote += 25;
            else if (insuree.CarYear > 2015)
                baseQuote += 25;

            if (insuree.CarMake.ToLower() == "porsche")
                baseQuote += 25;

            if (insuree.CarModel.ToLower() == "911 carrera")
                baseQuote += 25;

            baseQuote += insuree.SpeedingTickets * 10;

            if (insuree.DUI)
                baseQuote += baseQuote * 0.25m;

            if (insuree.CoverageType)
                baseQuote += baseQuote * 0.50m;

            return baseQuote;
        }
    }
}