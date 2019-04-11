using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB1
{
    public class MOrder
    {
        public int id { get; set; }
        public string em_target { get; set; }
        public string em_from { get; set; }
        public string type_id { get; set; }
        public string contents { get; set; }
        public string status { get; set; }
    }
}
