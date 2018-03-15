using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class VacationBalance
    {
        public int Annual { get; set; }

        public int Urgent { get; set; }

        public int Overtime { get; set; }

        public int Holyday { get; set; }

        public int Mng { get; set; }

        public int Priv { get; set; }
    }
}