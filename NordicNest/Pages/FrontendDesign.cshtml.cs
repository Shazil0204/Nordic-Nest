using NordicNest.Controller.DefaultSLController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NordicNest.Pages
{
    public class FrontendDesignModel : PageModel
    {


		public int SavingProgressBar = 59;
		public int LoanProgressBar = 70;
		public void OnGet()
        {
        }

        public decimal Savingpercentage()
        {
			DefaultSavingController DSC = new DefaultSavingController();
			var Sresult = DSC.GetData();

			decimal percentage = (Sresult.Item2 / Sresult.Item3) * 100;

			return percentage;
		}

		public decimal Loanpercentage()
		{
			DefaultLoanController DLC = new DefaultLoanController();
			var Lresult = DLC.GetData();

            decimal percentage = (Lresult.Item2 / Lresult.Item3) * 100;

            return percentage;
		}
	}
}
