using RAPLite.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RAPLite.DataSource
{
    public static class XmlAdapter
    {
        private static string filePath = "C:\\Users\\asus\\Documents\\Visual Studio 2015\\Projects\\RAPLitev2\\RAPLitev2\\DataSource\\PublicationRecord.xml";
        public static List<Publication> LoadAll()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);

            List<Publication> publications = new List<Publication>();

            XmlNodeList publicationNode = xml.SelectNodes("/Publications/Publication");
           
            foreach (XmlNode node in publicationNode)
            {
                Publication publication = new Publication();
                publication.Id = node.Attributes["id"].Value;
                publication.Title = node["Title"].InnerText;
                publication.YearOfPublication = node["Year"].InnerText;
                publication.AvailableFrom = DateTime.Parse(node["AvailableFrom"].InnerText); //Converting date in string to DateTime 
                publication.Type = (OutputType)Enum.Parse(typeof(OutputType), node["Type"].InnerText); //Converting string to Enumeration
                publication.Ranking = (OutputRanking)Enum.Parse(typeof(OutputRanking), node["Ranking"].InnerText); //Converting string to Enumeration 

                XmlNodeList authorNodes = node.SelectNodes("Author"); // Building a list of Author names as individual string items
                foreach (XmlNode authorNode in authorNodes)
                {
                    publication.AddAuthorName(authorNode.InnerText);
                }
                publications.Add(publication);
            }

            return publications;
        } 
    }
}   
