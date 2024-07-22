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


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,SpeedingTickets,HasDUI,IsFullCoverage")] Insuree insuree)
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

        private decimal CalculateQuote(Insuree insuree)
        {
            decimal quote = 50; // Base price

            int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
            if (insuree.DateOfBirth > DateTime.Now.AddYears(-age)) age--;

            if (age <= 18)
            {
                quote += 100;
            }
            else if (age >= 19 && age <= 25)
            {
                quote += 50;
            }
            else if (age >= 26)
            {
                quote += 25;
            }

            if (insuree.CarYear < 2000)
            {
                quote += 25;
            }
            if (insuree.CarYear > 2015)
            {
                quote += 25;
            }

            if (insuree.CarMake.ToLower() == "porsche")
            {
                quote += 25;
                if (insuree.CarModel.ToLower() == "911 carrera")
                {
                    quote += 25;
                }
            }

            quote += insuree.SpeedingTickets * 10;

            if (insuree.HasDUI)
            {
                quote *= 1.25m; // Add 25%
            }

            if (insuree.IsFullCoverage)
            {
                quote *= 1.50m; // Add 50%
            }

            return quote;
        }

        // Admin View
        public ActionResult Admin()
        {
            var insurees = db.Insurees.ToList();
            return View(insurees);
        }
    }
}