using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHS.StartupSimple
{
    public class MyOptions
    {
        public String option1 { get; set; }
        public String Option2 { get; set; }

        public Dictionary<String, String> subsection { get; set; }

        public List<WizardsModel> Wizards { get; set; }

    }

    public class WizardsModel
    {
        public String Name { get; set; }
        public String Age { get; set; }
    }
}
