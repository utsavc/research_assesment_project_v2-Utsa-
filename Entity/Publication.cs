using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPLite.Entity
{
    public class Publication
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<string> AuthorNames { get; set; }
        public OutputType Type { get; set; }
        public string YearOfPublication { get; set; }
        public DateTime AvailableFrom { get; set; }
        public OutputRanking Ranking { get; set; }




        /*
         * Assuming that a publication cant be annonymous, 
         * Whenever an Object of Publication is created
         * a list of string is initialised to add the author names
         * These author names (in this scenario) will match with the researcher names
         */
        public Publication()
        {
            AuthorNames = new List<string>();
        }

        /*
         * As having authors is a behaviour of the Publication object
         * therefore adding author name method should be created in the Publication object
         * instead of its controller
         */
        public void AddAuthorName(string authorName)
        {
            AuthorNames.Add(authorName);
        }

        /*
         * It is a good practice to override a ToString method for
         * any entity object. This helps to test the properties of the object
         * during testing
         */
        public override string ToString()
        {
            return Id + " - " + Title + " - " + AuthorNames[0] + " et. al";
        }

    }
    public enum OutputType
    {
        Conference, 
        Workshop,
        Journal
    }

    public enum OutputRanking
    {
        Q1,
        Q2,
        Q3,
        Q4
    }

}
