using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP.Models.Shopping
{
   public class SubTree3
    {
        public int TotalProducts { get; set; }
        public string Id { get; set; }
        public List<SubTree4> SubTree { get; set; }
        public int Depth { get; set; }
        public string ParentId { get; set; }
        public string Thumbnail { get; set; }
        public string Name { get; set; }
        public int SubTreeCount { get; set; }
    }
}
