using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TmpReportDesign
{
    public class ReportVM
    {
        public string ImePrezime { get; set; }
        public string Datum { get; set; }
        public string Iznos { get; set; }
        public string NacinPlacanja { get; set; }

        public static List<ReportVM> Get()
        {
            return new List<ReportVM> { };
        }
    }
}