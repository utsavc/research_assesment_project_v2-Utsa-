using MySql.Data.MySqlClient;
using System;
using RAPLite.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPLitev2.DataSource
{
    class PublicationDataSource
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private  MySqlConnection GetConnection()
        {
            string connectionString = $"Database={db};Data Source={server};User Id={user};Password={pass}";
            return new MySqlConnection(connectionString);
        }

        public  List<Publication> GetPublication()
        {
            List<Publication> publications = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT doi, title, authors, year, cite_as, type, available " +
                               "FROM publication";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        publications = new List<Publication>();

                        while (rdr.Read())
                        {
                            publications.Add(
                                new Publication()
                                {
                                    Id= rdr.GetString(0),
                                    Title= rdr.GetString(1),
                                    AuthorNames= new List<string> { rdr.GetString(2) },
                                    YearOfPublication=rdr.GetString(3),
                                    AvailableFrom=rdr.GetDateTime(6),
                                   //Type = (OutputType)Enum.Parse(typeof(OutputType), rdr.GetString(5)),
                                }
                                );
                        }
                    }
                    return publications;
                }
            }
        }
    }
}