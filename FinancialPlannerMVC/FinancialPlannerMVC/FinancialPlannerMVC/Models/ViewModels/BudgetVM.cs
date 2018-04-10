using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialPlannerMVC.Models.ViewModels
{
    public class BudgetVM
    {
        public Budget Budget { get; set; }
        public ICollection<BudgetItem> BudgetItems { get; set; }

    }
}