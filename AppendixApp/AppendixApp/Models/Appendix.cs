using System;
using System.Collections.Generic;
using System.Text;

namespace AppendixApp.Models
{
    public class Appendix
    {
        public int CompanyID { get; set; } = 0;
        public int UserID { get; set; } = 0;

        public string Text { get; set; } = "";

        public string Date { get; set; } = "";

        public int AccountNo { get; set; } = 0;

        public string ImageData { get; set; } = "";

        public string ImageFileName { get; set; } = "";
    }

}
