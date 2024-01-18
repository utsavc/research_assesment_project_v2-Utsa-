using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPLitev2.Entity
{
    public class Funding
    {
        public string ProjectId { get; set; }
        public int Amount { get; set; }
        public int Year { get; set; }

        public List<int> Researchers { get; set; }

        public Funding()
        {
            Researchers = new List<int>();
        }
    }
}
