using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RAPLite.Entity
{

    public enum Position
    {
        A,
        B,
        C,
        D,
        E,
        Student,
        All,
        Staff
    }

    public class Researcher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string CampusName { get; set; }
        public string Unit { get; set; }
        public string Email { get; set; }
        public Position EmploymentLevel { get; set; }
        public List<int> FundingList { get; set; }
        public Uri Photo { get; set; }
        public string Degree { get; set; }

        public int SupervisorId { get; set; }

        public string UtasStart { get; set; }
        public string CurrentStart { get; set; }
        public double FundingToMaintain { get; set; } // This property doesnt exist in RAP specs, just added to demo white box testing


        public List<Publication> PublicationList { get; set; }
        public override string ToString()
        {
            return Id + "-" + Title + "-" + Name;
        }

        public Researcher() { }
        public Researcher(int id, string name, string title, string campusName, Position employmentLevel, List<int> fundingList, double fundingToMaintain)
        {
            Id = id;
            Name = name;
            Title = title;
            CampusName = campusName;
            EmploymentLevel = employmentLevel;
            FundingList = fundingList;
            FundingToMaintain = fundingToMaintain;
        }


        public int Tenure
        {
            get
            {

                int r1 = int.Parse((DateTime.Now).ToString("yyyy"));                
                string d = UtasStart;
                DateTime date = DateTime.Parse(d);
                int r2 = date.Year;

                return r1-r2;
            }

        }

        public string CurrentJob
        {
            get
            {
                string level = EmploymentLevel.ToString();
                if (level == "A")
                    return "Post-Doc";
                if (level == "B")
                    return "Lecturer";
                if (level == "C")
                    return "SeniorLecturer";
                if (level == "D")
                    return "AssociateProfessor";
                if (level == "E")
                    return "Professor";


                else
                    return "student";
            }

        }

        public double CalculateThreeYearAverage()
        {
            List<Publication> researcherPublications = PublicationList;

            if (researcherPublications == null || !researcherPublications.Any())
            {
                return 0.0;
            }
            
            int currentYear = DateTime.Now.Year;
            
            var relevantPublications = researcherPublications
                .Where(p => Convert.ToInt32(p.YearOfPublication) >= currentYear - 3);
            
            int totalPublications = relevantPublications.Count();
            
            double threeYearAverage = totalPublications / 3.0;

            return threeYearAverage;
        }

        public int PublicationCount
        {
            get
            {
                if (PublicationList == null)
                    return 0;
                else
                    return PublicationList.Count();
            }
        }
    }

    public enum Campus
    {
        SandyBay, Newnham, Burny
    }



  


}
