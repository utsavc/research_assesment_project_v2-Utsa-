using MySql.Data.MySqlClient;
using RAPLite.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPLitev2.DataSource
{
    class ResearcherDataSource
    {
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private MySqlConnection GetConnection()
        {
            string connectionString = $"Database={db};Data Source={server};User Id={user};Password={pass}";
            return new MySqlConnection(connectionString);
        }

        public List<Researcher> ShowResearcher()
        {
            List<Researcher> researchers = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "SELECT id, type, given_name, family_name, title, unit, campus, email, photo, degree, supervisor_id,level, utas_start, current_start FROM researcher";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        researchers = new List<Researcher>();
                        while (reader.Read())
                        {
                            string name = reader.GetString(3) + " " + reader.GetString(2)+" ,"+ reader.GetString(4);
                                
                            researchers.Add(
                           new Researcher()
                           {
                               Id = reader.GetInt32(0),
                               lastName=reader.GetString(2),
                               Type=reader.GetString(1),
                               EmploymentLevel = !reader.IsDBNull(11) ? (Position)Enum.Parse(typeof(Position), reader.GetString(11)) : Position.All,
                               Name = name,
                               Title = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                               Unit = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                               CampusName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                               Email = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                               Photo = new Uri(reader.IsDBNull(8) ? string.Empty : reader.GetString(8), UriKind.RelativeOrAbsolute),
                               Degree = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                               SupervisorId = reader.IsDBNull(10) ? -1 : reader.GetInt32(10),
                               UtasStart = reader.IsDBNull(12) ? string.Empty : reader.GetString(12),
                               CurrentStart = reader.IsDBNull(13) ? string.Empty : reader.GetString(13)
                     
                               
                           }

                            );
                        }
                    }


                    return researchers;
                }
            }
        }


        
    }
}
