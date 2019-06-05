using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab28.Models
{
    public class Cards
    {
        public List<string> CardsList { get; set; }
        public string image { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
        public string code { get; set; }
    }
}