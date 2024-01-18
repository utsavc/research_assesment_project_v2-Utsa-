using RAPLite.DataSource;
using RAPLite.Entity;
using RAPLitev2.DataSource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RAPLite.Controller
{
    /*
     * A class without state doenst need 
     * to be treated as an object. Therefore i
     * have used static keyword to create a controller 
     * class. 
     * Controllers are a good example of Static classes and
     * methods (aka class methods)
     */
    public class PublicationController
    {
        /* private means it is only accessible within this class
         * as controllers are mostly functions/methods
         * its better to keep the fields private
         * and only return if needed via static method
         */
        private static  List<Publication> publications;
        public  List<Publication> LoadAllPublications()
        {
            publications = new PublicationDataSource().GetPublication();
            if (publications != null)
            {
                return publications;
            }
            return null;
        }
        public static List<Publication> SearchByTitle(string title) 
        {
            var pubsByTitle = from pub in publications
                              where pub.Title == title
                              select pub;

            return (List<Publication>) pubsByTitle.ToList(); 
        }
        public  List<Publication> SearchByResearcher(Researcher researcher)
        {
            if (researcher == null)
            {
                return publications;
            }

            if (publications == null)
            {
                publications = new PublicationController().LoadAllPublications();
            }
            /*
             * Althought C# is a typed language
             * it does support dynamic typing 
             * like in the following code where a dynamically 
             * typed variable of type var is declared and returned
             * after typecasting
             */
            var pubsByAuthor = from pub in publications
                               from author in pub.AuthorNames  // nested LINQ as there is a list within a list
                               where author.Equals(researcher.Name)
                               select pub;

            // publications = new List<Publication>(pubsByAuthor);

            /* 
             * Foreach equivalent of LINQ above
             */
            /* List<Publication> filteredPublications = new List<Publication>();
             foreach(Publication pub in publications)
             {
                 foreach(string name in pub.AuthorNames)
                 {
                     if (name.Equals(authorName)) 
                     {
                         filteredPublications.Add(pub);
                     }
                 }
             }
             return filteredPublications;
            */

            return (new List<Publication>(pubsByAuthor));// dynamic typed variable has to be converted(casted) into a strong type (List<Publication>) before return

        }

        public int CountPublicationsByResearcher(Researcher researcher)
        {
            if (researcher == null)
            {
                return 0; // Return 0 if the researcher is null
            }

            if (publications == null)
            {
                publications = new PublicationController().LoadAllPublications();
            }

            // Use LINQ to count the publications for the specified researcher
            int publicationCount = publications.Count(pub => pub.AuthorNames.Contains(researcher.Name));

            return publicationCount;
        }


        public static double CalculateThreeYearAverage(List<Publication> publications, int currentYear)
        {
            // Filter publications from the previous three years
            var relevantPublications = publications.Where(p => Convert.ToInt32(p.YearOfPublication) >= currentYear - 3);

            // Calculate the total number of publications
            int totalPublications = relevantPublications.Count();

            // Calculate the 3-year average
            double threeYearAverage = totalPublications / 3.0;

            return threeYearAverage;
        }


        public static List<Publication> SearchByResearcher(string researcher)
        {
            /*
             * Althought C# is a typed language
             * it does support dynamic typing 
             * like in the following code where a dynamically 
             * typed variable of type var is declared and returned
             * after typecasting
             */
            var pubsByAuthor = from pub in publications
                               from author in pub.AuthorNames  // nested LINQ as there is a list within a list
                               where author.Equals(researcher)
                               select pub;

            /* 
             * Foreach equivalent of LINQ above
             */
            /* List<Publication> filteredPublications = new List<Publication>();
             foreach(Publication pub in publications)
             {
                 foreach(string name in pub.AuthorNames)
                 {
                     if (name.Equals(authorName)) 
                     {
                         filteredPublications.Add(pub);
                     }
                 }
             }
             return filteredPublications;
            */

            return (List<Publication>)pubsByAuthor.ToList(); // dynamic typed variable has to be converted(casted) into a strong type (List<Publication>) before return

        }
    }
}
