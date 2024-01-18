    using System.Text;
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using RAPLite.Entity;
    using System.Xml;
    using RAPLitev2.Entity;

    namespace RAPLitev2.DataSource
    {
        public static class FundingDataSource
        {
        private static string filePath = "C:\\Users\\HP\\Documents\\Visual Studio\\Projects\\RAPLitev2\\RAPLitev2\\DataSource\\Fundings_Rankings.xml";

        public static List<Funding> LoadAll()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);

            List<Funding> fundings = new List<Funding>();

            XmlNodeList projectNodes = xml.SelectNodes("/Projects/Project");

            foreach (XmlNode projectNode in projectNodes)
            {
                Funding funding = new Funding();
                funding.ProjectId = projectNode.Attributes["id"].Value;
                funding.Amount = int.Parse(projectNode["Funding"].InnerText);
                funding.Year = int.Parse(projectNode["Year"].InnerText);

                XmlNodeList researcherNodes = projectNode.SelectNodes("Researchers/staff_id");
                foreach (XmlNode researcherNode in researcherNodes)
                {
                    funding.Researchers.Add(int.Parse(researcherNode.InnerText));
                }

                fundings.Add(funding);
            }

            return fundings;
        }
    }
    }