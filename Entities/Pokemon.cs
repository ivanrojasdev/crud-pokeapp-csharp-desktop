using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Pokemon
    {
        public int PokemonId {  get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public string UrlImage { get; set; }

    }
}
