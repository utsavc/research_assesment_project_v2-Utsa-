using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAPLitev2.DataSource;
using RAPLitev2.Entity;

namespace RAPLitev2.Controller
{
    public static class FundingsController
    {
        public static List<Funding> GetResearcherFundings(int researcherId, List<Funding> allFundings)
        {
            // Filter fundings for the selected researcher
            return allFundings.Where(funding => funding.Researchers.Contains(researcherId)).ToList();
        }

        public static double CalculateTotalFunding(List<Funding> fundings)
        {
            // Calculate the sum of funding
            return fundings.Sum(funding => funding.Amount);
        }
    }
}
