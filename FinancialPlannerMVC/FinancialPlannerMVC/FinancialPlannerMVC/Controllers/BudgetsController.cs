using FinancialPlannerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancialPlannerMVC.Controllers
{
    public class BudgetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Budgets
        public ActionResult Index()
        {
            return View();
        }

        // POST: create budget
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Budget budget)
        {
            if (ModelState.IsValid)
            {
                budget.IsDeleted = false;
                db.Budgets.Add(budget);
                db.SaveChanges();

                return RedirectToAction("BudgetDetails","BudgetView",new { budgetId = budget.Id });
            }

            //FIX THIS
            return View();
        }


    }
}