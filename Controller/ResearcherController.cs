using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RAPLite.Entity;
using RAPLite.Controller;
using RAPLitev2.DataSource;

namespace RAPLite.Controller
{
    public static class ResearcherController
    {
        private static List<Researcher> tempResearcherList;
        public static List<Researcher> ResearchersList;
        public static string empLevel;
        public static PerformanceLevel getPerformance(Researcher researcher)
        {
            int totalFunding = 0;
            foreach(int funding in researcher.FundingList)
            {
                totalFunding += funding;
            }

            double fundingRatio = researcher.FundingToMaintain/totalFunding; // Performance calculation method in your RAP case study is much simpler,
                                                                             // this is just for example

            if (fundingRatio >= 2) 
            {
                return PerformanceLevel.Poor;
            }else if (fundingRatio > 1)
            {
                return PerformanceLevel.Below_Average;
            }else if (fundingRatio == 1)
            {
                return PerformanceLevel.Average;
            }else if (fundingRatio < 1)
            {
                return PerformanceLevel.Good;
            }else if (fundingRatio < 0.5)
            {
                return PerformanceLevel.Excellent;
            }

            return PerformanceLevel.Poor;
        }

        public static List<Researcher> FilterByType(bool isStudent, ObservableCollection<Researcher> researchers)
        {
            if (isStudent)
            {
                var filtered = from Researcher researcher in researchers
                               where researcher.EmploymentLevel == Position.Student
                               select researcher;
                tempResearcherList = new List<Researcher>(filtered);
            }
            else
            {
                var filtered = from Researcher researcher in researchers
                               where researcher.EmploymentLevel != Position.Student
                               select researcher;
                tempResearcherList = new List<Researcher>(filtered);
            }
            return tempResearcherList;
        }


        public static string GetResearcherNameBySupervisorId(int supervisorId, ObservableCollection<Researcher> researchers)
        {
            var researcher = researchers.FirstOrDefault(r => r.Id == supervisorId);

            // If researcher is found, return the name; otherwise, return a default value
            return researcher != null ? researcher.Name : "";
        }

        public static List<Researcher> FilterByTypeAndLevel(Position level, string type, ObservableCollection<Researcher> researchers)
        {
            List<Researcher> filteredList;

            if (type.EndsWith("Student"))
            {
                // Filter students regardless of the level based on the Type field
                filteredList = researchers.Where(r => r.Type.EndsWith("Student")).ToList();
            }
            else if (type.EndsWith("Staff"))
            {
                // Filter staff regardless of the level
                filteredList = researchers.Where(r => r.Type.EndsWith("Staff")).ToList();
            }
            else if (type.EndsWith("All"))
            {
                // Filter staff regardless of the level
                filteredList = researchers.Where(r => r.Type.EndsWith("Staff")|| r.Type.EndsWith("Student")).ToList();
            }
            else
            {
                // If "All" or any other type is selected, filter only by the selected level
                if (level == Position.A)
                {
                    filteredList = researchers.Where(r => r.EmploymentLevel == Position.A).ToList();
                }
                else if (level == Position.B)
                {
                    filteredList = researchers.Where(r => r.EmploymentLevel == Position.B).ToList();
                }
                else if (level == Position.C)
                {
                    filteredList = researchers.Where(r => r.EmploymentLevel == Position.C).ToList();
                }
                else if (level == Position.D)
                {
                    filteredList = researchers.Where(r => r.EmploymentLevel == Position.D).ToList();
                }
                else if (level == Position.E)
                {
                    filteredList = researchers.Where(r => r.EmploymentLevel == Position.E).ToList();
                }
                else
                {
                    filteredList = researchers.Where(r => r.Type.EndsWith("Staff") || r.Type.EndsWith("Student")).ToList();
                }
            }

            return filteredList;
        }


        public static List<Researcher> FilterResearchersByLastName(string searchText, ObservableCollection<Researcher> researchers)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return researchers.ToList();
            }

            string searchLower = searchText.ToLower();

            var filteredResearchers = researchers.Where(r => r.lastName != null && r.Name.ToLower().StartsWith(searchLower)).ToList();

            return filteredResearchers;
        }



        public static int CountSupervisions(Researcher researcher, ObservableCollection<Researcher> allResearchers)
        {
            if (researcher == null || allResearchers == null)
            {
                return 0;
            }

            int supervisorId = researcher.Id;

            int supervisionCount = allResearchers.Count(r => r.SupervisorId == supervisorId);

            return supervisionCount;
        }







    }

    public enum PerformanceLevel
    {
        Poor, Below_Average, Average, Good, Excellent
    }


}
