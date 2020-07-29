using System;
using System.Collections.Generic;
using System.Text;

namespace AppendixApp.Models
{
    public class StatusResponse
    {
        public int Status { get; set; } = 0;
        public string Message { get; set; } = "";
        public int AppendixID { get; set; } = 0;
    }
}
