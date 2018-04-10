using FinancialPlannerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancialPlannerMVC.Controllers.vmControllers
{
    public class BudgetViewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BudgetView
        [HttpGet]
        public ActionResult BudgetDetails(int budgetId)
        {
            Budget budget = db.Budgets.Find(budgetId);

            if (budget == null)
            {
                return HttpNotFound();
            }


            return View();



            return View();
        }
    }
}