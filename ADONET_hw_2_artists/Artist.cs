using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONET_hw_2_artists
{
    public class Artist 
    {
        public int Id { get; }
        public string Name { get; set; }

        public Artist(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Artist()
        {

        }
    }
}
